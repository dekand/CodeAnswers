# CodeAnswers
Сайт pet-проект - аналог сайта StackOverflow.com
## Технологии
1) C#, .NET 8 Core
2) HTML, CSS + bootstrap, JS + jquery
3) MVC
4) MS SQL Server (local)
5) Entity Framework (ver 8)
## Схема базы данных
![CodeAnswersDB](pictures/CodeAnswersDB.svg)
### Функции вычисляемых столбцов БД
Для расчета репутации пользователя извлекается рейтинги всех его вопросов и ответов, суммируются. Эти действия выполняются функцией БД `Fun_ReputationCalc(@Id INT)`, где `@Id` - идентификатор пользователя.
```sql
CREATE FUNCTION Fun_ReputationCalc(@Id INT)
 RETURNS INT
 AS BEGIN
  DECLARE  @q INT, @a INT
  SET @q = ISNULL((SELECT SUM(Questions.rating) FROM Questions WHERE Questions.author_id=@Id),0)
  SET @a = ISNULL((SELECT SUM(Answers.rating) FROM Answers WHERE Answers.author_id=@Id),0)
  RETURN (@a+@q)
 END
```
Столбец `Answered` таблицы вопросов  имеет значение `TRUE`, если у вопроса есть хотя бы один ответ, помеченный, как "решение". Для этого создана функция `Fun_Answered(@Id INT)`, где `@Id` - идентификатор вопроса.
```sql
CREATE FUNCTION Fun_Answered(@Id INT)
 RETURNS BIT
 AS BEGIN
  IF ((SELECT COUNT(*) AS num_answ FROM Answers WHERE Answers.question_id = @Id AND Answers.accepted=CONVERT([bit],(1))) > 0)
  RETURN (CONVERT([bit],(1)))
  RETURN (CONVERT([bit],(0)))
 END
```
### Триггеры БД
При создании/удалении пользователя в таблице `AspNetUsers` срабатывает триггер, добавляющий/удаляющий пользовтеля в таблицу `Users`, где хранится дополнительная информация о пользователе.
```sql
CREATE TRIGGER [DeleteUser]
	ON [dbo].[AspNetUsers]
	AFTER DELETE
	AS
	BEGIN
		DELETE FROM Users 
		WHERE Users.name=(SELECT UserName from deleted) 
		AND Users.email=(SELECT Email from deleted)
	END
```
```sql
CREATE TRIGGER [AddNewUser]
	ON [dbo].[AspNetUsers]
	AFTER INSERT
	AS
	BEGIN
		INSERT INTO Users (name, email)
		SELECT UserName, Email
		FROM INSERTED
	END
```
Аналогично при добавлении пользователя в `Users` срабатывает тригер по созданию записи в таблице `Images` (изображение по умолчанию). Если пользователь будет удален из таблицы `Users`, будет произведено каскадное удаление всей информации, которая с ним связана.
```sql
CREATE TRIGGER [AddDefaultImage]
	ON [dbo].[Users]
	AFTER INSERT
	AS
	BEGIN
		INSERT INTO Images (user_id)
		SELECT id
		FROM INSERTED
	END
```
