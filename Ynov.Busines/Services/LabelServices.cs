using Ynov.Business.Exceptions;
using Ynov.Business.IRespositories;
using Ynov.Business.IServices;
using Ynov.Business.Models;

namespace Ynov.Business.Services;

public class LabelServices : ILabelServices
{
    private readonly ILabelRepository _labelRepository;

    public LabelServices(ILabelRepository labelRepository)
    {
        _labelRepository = labelRepository;
    }
    
    
    public BusinessResult<List<Label>> Get()
    {
        List<Label> labels = _labelRepository.Get();

        return BusinessResult<List<Label>>.FromSuccess(labels);
    }
    
    public BusinessResult<Label> Get(long id)
    {
        Label? label = _labelRepository.Get(id);

        if (label is null)
        {
            return BusinessResult<Label>.FromError($"The label {id} do not exist", BusinessErrorReason.NotFound);
        }

        return BusinessResult<Label>.FromSuccess(label);
    }
    
    public BusinessResult<Label> Add(Label lLabel)
    {
        try
        {
            Label? label = _labelRepository.Add(lLabel);
            if (label is null)
            {
                return BusinessResult<Label>.FromError("Error while adding label", BusinessErrorReason.BusinessRule);
            }

            return BusinessResult<Label>.FromSuccess(label);
        }
        catch (LabelAlreadyExistsException ex)
        {
            return BusinessResult<Label>.FromError(ex.Message, BusinessErrorReason.DbConflict);
        }
    }

    public BusinessResult<Label> Modify(long id, Label mLabel)
    {
        Label? label = _labelRepository.Get(id);

        if (label is null)
        {
            return BusinessResult<Label>.FromError($"The label {id} do not exist", BusinessErrorReason.NotFound);
        }

        try
        {
            label.Name = mLabel.Name;
            _labelRepository.Modify(label);
            return BusinessResult<Label>.FromSuccess(label);
        }
        catch (LabelAlreadyExistsException e)
        {
            return BusinessResult<Label>.FromError(e.Message, BusinessErrorReason.DbConflict);
        }
    }

    public BusinessResult Delete(long id)
    {
        Label? label = _labelRepository.Get(id);

        if (label is null)
        {
            return BusinessResult<Label>.FromError($"The label {id} do not exist", BusinessErrorReason.NotFound);
        }
        _labelRepository.Delete(label);
        return BusinessResult.FromSuccess();
    }
}