using Ynov.Business.DTOitem;
using Ynov.Business.Models;

namespace Ynov.Business.IRespositories;

public interface ILabelRepository
{
    public List<Label> Get();
    public Label? Get(long id);
    public Label Add(Label board);
    public Label? Modify(Label board);
    public void Delete(Label board);
}