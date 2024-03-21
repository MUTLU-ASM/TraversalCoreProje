using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class GuideValidator : AbstractValidator<Guide>
    {
        public GuideValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad Soyad kısmını boş geçemezsiniz...!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Rehber Açıklama kısmını boş geçemezsiniz...!");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Rehber Fotoğraf kısmını boş geçemezsiniz...!");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Lütfen 30 karakterden daha kısa bir isim giriniz...!");
        }
    }
}
