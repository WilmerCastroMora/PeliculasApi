using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Validaciones
{
    public class TipoArchivoValidacion:ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public TipoArchivoValidacion(string[] tiposValidos)
        {
            this.tiposValidos = tiposValidos;
        }
        public TipoArchivoValidacion(GrupoTipoArchivo grupoTipoArchivo)
        {
            if(grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                tiposValidos = new string[] { "image/jpeg", "image/gif", "image/png" };
            }
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
            if (!tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Los tipos de archivos permitidos son: {string.Join(", ", tiposValidos)}");
            }
            return ValidationResult.Success;
        }
    }
}
