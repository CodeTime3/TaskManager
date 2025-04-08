using Microsoft.EntityFrameworkCore;
using TaskManagerDataAccess.Models;
using ZstdSharp.Unsafe;

namespace TaskManagerDataAccess.DataAccess.Services;

public class NoteService
{   
    private readonly TaskManagerDbContext _dbContext;

    public NoteService (TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Note> AddNote (Note note)
    {
        ArgumentNullException.ThrowIfNull(note);

        _dbContext.Add(note);
        await _dbContext.SaveChangesAsync();

        return note;
    }

    public async Task<Note[]> GetNotesByTaskId (int id)
    {
        var notes = await _dbContext.Notes
            .Where(n => n.ProjectTaskId == id)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(notes);

        return notes.ToArray();
    }

    public async Task<Note> UpdateNote (Note note)
    {
        ArgumentNullException.ThrowIfNull(note);

        _dbContext.Update(note);
        await _dbContext.SaveChangesAsync();

        return note;
    }

    public async Task DeleteNoteById (int id)
    {
        var note = await _dbContext.Notes.FindAsync(id);

        ArgumentNullException.ThrowIfNull(note);

        _dbContext.Remove(note);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllNotes (int id)
    {
        var notes = await _dbContext.Notes
            .Where(n => n.ProjectTaskId == id)
            .ToListAsync();

        ArgumentNullException.ThrowIfNull(notes);

        _dbContext.RemoveRange(notes);
        await _dbContext.SaveChangesAsync();
    }
}