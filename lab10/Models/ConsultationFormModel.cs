using System;
using System.ComponentModel.DataAnnotations;

namespace lab10.Models
{
    public class ConsultationFormModel
    {
        [Required(ErrorMessage = "Ім'я та прізвище є обов'язковими.")]
        [StringLength(100, ErrorMessage = "Ім'я та прізвище повинні містити не більше 100 символів.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email є обов'язковим.")]
        [EmailAddress(ErrorMessage = "Невірний формат Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Дата консультації є обов'язковою.")]
        [DataType(DataType.Date, ErrorMessage = "Невірний формат дати.")]
        [FutureDate(ErrorMessage = "Дата консультації має бути в майбутньому.")]
        [NoWeekends(ErrorMessage = "Консультації не проводяться у вихідні дні.")]
        public DateTime? ConsultationDate { get; set; }

        [Required(ErrorMessage = "Будь ласка, виберіть продукт.")]
        public string Product { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now.Date;
            }
            return false;
        }
    }

    public class NoWeekendsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                var dayOfWeek = date.DayOfWeek;
                return dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday;
            }
            return false;
        }
    }
}
