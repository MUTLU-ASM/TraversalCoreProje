using DTOLayer.DTOs.AppUserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AppUserRegisterValidator : AbstractValidator<AppUserRegisterDTO>
    {
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad kısmını boş geçemezsiniz...!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad kısmını boş geçemezsiniz...!");
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail kısmını boş geçemezsiniz...!");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı Ad kısmını boş geçemezsiniz...!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre kısmını boş geçemezsiniz...!");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrar kısmını boş geçemezsiniz...!");
            RuleFor(x => x.Username).MinimumLength(5).WithMessage("En az 5 karakter giriniz...!");
            RuleFor(x => x.Username).MaximumLength(25).WithMessage("En fazla 25 karakter giriniz...!");
            RuleFor(x => x.Password).Equal(x=>x.ConfirmPassword).WithMessage("Şifreler birbirleriyle uyuşmuyor...!");
        }
    }
}
