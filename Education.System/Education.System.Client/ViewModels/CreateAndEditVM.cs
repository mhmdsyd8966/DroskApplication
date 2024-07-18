using System.ComponentModel.DataAnnotations;
using Education.System.Core.Dto.ApplicationDto;

namespace Education.System.Client.ViewModels;

public class CreateAndEditVM<T> where T : class
{
    [Required]
    public T? Model { set; get; }
    [Required]
    public Guid Id { set; get; }

    public CreateAndEditVM(T? model, Guid id)
    {
        Model = model;
        Id = id;
    }

    public CreateAndEditVM(Guid id)
    {
        Model = default;
        Id = id;
    }

    public CreateAndEditVM()
    {

    }
}