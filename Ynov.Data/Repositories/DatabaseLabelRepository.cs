using Microsoft.EntityFrameworkCore;
using Ynov.Business.DTOitem;
using Ynov.Business.Exceptions;
using Ynov.Business.IRespositories;
using Ynov.Business.Models;
using Ynov.Data.Contexts;

namespace Ynov.Data.Repositories;

public class DatabaseLabelRepository : ILabelRepository
{
    private readonly TrellodDbContext _context;

    public DatabaseLabelRepository(TrellodDbContext labelDbContext)
    {
        _context = labelDbContext;
    }

    public List<Label> Get()
    {
        return _context.Labels.ToList();
    }

    public Label? Get(long id)
    {
        return _context.Labels.FirstOrDefault(l => l.Id == id);
    }

    public Label Add(Label label)
    {
        var unique = _context.Labels.FirstOrDefault(l => l.Name == label.Name);

        if (unique is null)
        {
            _context.Labels.Add(label);
            _context.SaveChanges();
            return label;
        }

        throw new LabelAlreadyExistsException($"The label {label.Name} already exists.");
    }

    public Label? Modify(Label lLabel)
    {
        var label = _context.Labels.Find(lLabel.Id);
        var unique = _context.Labels.FirstOrDefault(l => l.Name == lLabel.Name);
        
        if (label != null)
        {
            if (unique is null)
            {
                label.Name = lLabel.Name;
                _context.SaveChanges();
            }
            else
            {
                throw new LabelAlreadyExistsException($"The label {label.Name} already exists.");
            }
        }

        return label;
    }

    public void Delete(Label label)
    {
        _context.Labels.Remove(label);
        _context.SaveChanges();
    }
}