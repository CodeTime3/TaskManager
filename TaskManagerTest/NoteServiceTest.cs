using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.DataAccess;
using TaskManagerDataAccess.DataAccess.Services;
using TaskManagerDataAccess.Models;

namespace TaskManagerTest;

public class NoteServiceTest
{
    [Fact]
    public async Task AddNote_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Note note = new Note(7, "hh", "hh");
        NoteService noteService = new NoteService(context);
        await noteService.AddNote(note);

        var notesInDb = context.Notes.ToArray();

        Assert.Equal("hh", notesInDb[0].NoteTitle);
        Assert.Equal("hh", notesInDb[0].NoteText);
    }

    [Fact]
    public async Task GetNotesByTaskId_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Note note = new Note();
        NoteService noteService = new NoteService(context);
        await noteService.GetNotesByTaskId(7);

        var notesInDb = context.Notes.ToArray();

        Assert.Equal(3, notesInDb.Length);
    }

    [Fact]
    public async Task UpdateNote_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Note note = new Note(7, "kk", "kk");
        NoteService noteService = new NoteService(context);
        note.NotedId = 3;
        note = await noteService.UpdateNote(note);

        Assert.Equal("kk", note.NoteTitle);
        Assert.Equal("kk", note.NoteText);
    }

    [Fact]
    public async Task DeleteNoteById_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Note note = new Note();
        NoteService noteService = new NoteService(context);
        await noteService.DeleteNoteById(3);

        var notesInDb = context.Notes.ToArray();

        Assert.Equal(2, notesInDb.Length);
    }

    [Fact]
    public async Task DeleteAllNotes_should_work()
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseMySQL("server=localhost;uid=root;pwd=CT22d03p06;database=TaskManager")
            .Options;

        using var context = new TaskManagerDbContext(options);
        Note note = new Note();
        NoteService noteService = new NoteService(context);
        await noteService.DeleteAllNotes(7);

        var notesInDb = context.Notes.ToArray();

        Assert.Equal(0, notesInDb.Length);
    }
}
