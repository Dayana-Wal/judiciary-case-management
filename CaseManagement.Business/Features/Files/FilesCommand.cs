using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace CaseManagement.Business.Features.Files
{
    public class FilesCommand
    {
        public int FileTypeId { get; set; } 
        public string UploadedBy { get; set; } = null!;
        public List<IFormFile> Files { get; set; } = null!;

        public ValidationResult ValidateCommand()
        {
            FileValidator validator = new FileValidator();
            return validator.Validate(this);
        }

    }

    public class FileValidator : AbstractValidator<FilesCommand> {
        public FileValidator()
        {
            RuleFor(model => model.UploadedBy).NotEmpty().WithMessage("Uploaded should not be empty");
            RuleFor(model => model.FileTypeId).NotEmpty().WithMessage("FileTypeId is required");
            RuleFor(model => model.Files)
                .NotEmpty()
                .WithMessage("Files should not be empty")
                .ForEach(rule =>
                {
                    rule.Must(HaveExtension)
                    .WithMessage("Invalid file extension, only allowed jpg,png and pdf files")
                    .Must(HaveValidSize)
                    .WithMessage("File size should be less than 2 MB.");

                });
        }

        private bool HaveExtension(IFormFile file) {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension);
        }

        private bool HaveValidSize(IFormFile file) { 
            const long maxFileSize = 2 * 1024 * 1024;
            return file.Length <= maxFileSize;
        }
    }
}
