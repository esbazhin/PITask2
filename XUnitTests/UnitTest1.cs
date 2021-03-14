using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PITask2;
using PITask2.Controllers;
using Xunit;

namespace XUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void CheckGetById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NotesDbContext>().UseInMemoryDatabase(databaseName: "NotesDb").Options;
            var db = new NotesDbContext(options);
            var note = new Note
            {
                Title = "Test",
                Content = "Test"
            };
            db.Notes.Add(note);
            db.SaveChanges();
            NotesController controller = new NotesController(db);
            // Act
            var result = controller.GetNote(1).Result.Value;
            // Assert
            Assert.NotNull(result);
            Assert.Equal(note.Title, result.Title);
            Assert.Equal(note.Content, result.Content);
        }

        [Fact]
        public void CheckGetWithoutTitle()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NotesDbContext>().UseInMemoryDatabase(databaseName: "NotesDb1").Options;
            var db = new NotesDbContext(options);
            var note = new Note
            {
                Content = "Test"
            };
            db.Notes.Add(note);
            db.SaveChanges();
            NotesController controller = new NotesController(db);
            // Act
            var result = controller.GetNote(1).Result.Value;
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tes", result.Title);
            Assert.Equal(note.Content, result.Content);
        }

        [Fact]
        public void CheckQuery()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NotesDbContext>().UseInMemoryDatabase(databaseName: "NotesDb").Options;
            var db = new NotesDbContext(options);
            var note1 = new Note
            {
                Title = "Test1",
                Content = "Test"
            };
            db.Notes.Add(note1);
            var note2 = new Note
            {
                Title = "Test2",
                Content = "Test"
            };
            db.Notes.Add(note2);
            db.SaveChanges();
            NotesController controller = new NotesController(db);
            // Act
            var result = controller.GetNotes(note1.Title).Result.Value.ToList();
            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(note1.Title, result[0].Title);
        }

        [Fact]
        public void CheckPut()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NotesDbContext>().UseInMemoryDatabase(databaseName: "NotesDb").Options;
            var db = new NotesDbContext(options);
            var note = new Note
            {
                Title = "Test1",
                Content = "Test"
            };            
            db.Notes.Add(note);
            db.SaveChanges();
            NotesController controller = new NotesController(db);
            // Act
            var newNote = new Note
            {
                Title = "new",
                Content = " new str"
            };
            var res = controller.PutNote(1, newNote).Result;
            var result = controller.GetNote(1).Result.Value;
            // Assert
            Assert.NotNull(result);
            Assert.Equal(newNote.Title, result.Title);
            Assert.Equal(newNote.Content, result.Content);
        }

        [Fact]
        public void CheckDelete()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NotesDbContext>().UseInMemoryDatabase(databaseName: "NotesDb").Options;
            var db = new NotesDbContext(options);
            var note = new Note
            {
                Title = "Test1",
                Content = "Test"
            };
            db.Notes.Add(note);
            db.SaveChanges();
            NotesController controller = new NotesController(db);
            // Act            
            var res = controller.DeleteNote(1).Result;
            var result = db.Find<Note>(1);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CheckPost()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NotesDbContext>().UseInMemoryDatabase(databaseName: "NotesDb").Options;
            var db = new NotesDbContext(options);
            var note = new Note
            {
                Title = "Test1",
                Content = "Test"
            };
            NotesController controller = new NotesController(db);
            // Act            
            var res = controller.PostNote(note).Result.Value;
            var result = db.Find<Note>(res.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(note.Title, result.Title);
            Assert.Equal(note.Content, result.Content);
        }
    }
}
