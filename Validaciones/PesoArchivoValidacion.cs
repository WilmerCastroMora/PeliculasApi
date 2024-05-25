using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Validaciones
{
    public class PesoArchivoValidacion : ValidationAttribute
    {
        private readonly int pesoMaxMbs;

        public PesoArchivoValidacion(int PesoMaxMbs)
        {
            pesoMaxMbs = PesoMaxMbs;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formFile = value as IFormFile;
            if (formFile == null)
            {
                return ValidationResult.Success;
            }
            if (formFile.Length > pesoMaxMbs * 1024 * 1024)
            {
                return new ValidationResult($"El tamaño del archivo no debe ser mayor a {pesoMaxMbs} Mbs");
            }
            return ValidationResult.Success;
        }
    }
}
