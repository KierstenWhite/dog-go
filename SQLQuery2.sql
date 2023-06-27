SELECT Walks.Date, Owner.Name, Walks.Duration
FROM Walks
JOIN Walker ON Walks.WalkerId = Walker.Id
JOIN Dog ON Walks.DogId = Dog.Id
JOIN Owner ON Dog.OwnerId = Owner.Id