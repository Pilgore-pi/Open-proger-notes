
```csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Multiverbum.Models;

/* [Required] - NOT NULL
 * <nothing> and using Nullable<T> type - NULL
 * [MaxLength(N), MinLength(N)] - CHECK(length(col) <= N AND length(col) >= N)
 * [Range(min, max)] - CHECK(col >= min AND col <= max) // INTEGER
 * [Key] - PRIMARY KEY
 * [ForeignKey("FKey")] - данное свойство является ссылкой на другую таблицу,
 *     для этого свойства должен быть определен внешний ключ "FKey" в этом же классе
 * [Unique] - UNIQUE
 */

public class UserProfile
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50), MinLength(2)]
    public string Username { get; set; }

    [Required, MaxLength(50), MinLength(3)]
    public string Password { get; set; }

    [Required]
    public UserStatus Status { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int TheoreticalMaterialsFinished { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int PracticalMaterialsFinished { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int PhoneticMaterialsFinished { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int AverageScoresPractical { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int AverageScoresPhonetic { get; set; }
}

public enum UserStatus
{
    Admin,
    Creator,
    Student
}

public class Session
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime WhenCreated { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int Scores { get; set; }

    [Required, Range(0, 1)]
    public int IsInterrupted { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public int StudyMaterialId { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int LastSection { get; set; }

    [ForeignKey("StudyMaterialId")]
    public virtual StudyMaterial StudyMaterial { get; set; }
    [ForeignKey("UserProfileId")]
    public virtual UserProfile UserProfile { get; set; }
}

public class ScheduledMaterial
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StudyMaterialId { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public DateTime PlannedDateTime { get; set; }

    [Required, Range(0, 1)]
    public int IsExpired { get; set; }

    [Range(0, 1)]
    public int? DoneInTime { get; set; }

    [ForeignKey("StudyMaterialId")]
    public virtual StudyMaterial StudyMaterial { get; set; }

    [ForeignKey("UserProfileId")]
    public virtual UserProfile UserProfile { get; set; }
}

public enum StudyMaterialType
{
    Theoretical,
    Practical,
    Phonetic
}

public class StudyMaterial
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CreatorId { get; set; }

    [Required]
    public StudyMaterialType Type { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public DateTime WhenCreated { get; set; }

    [MaxLength(500), MinLength(1)]
    public string? Description { get; set; }

    [MaxLength(150), MinLength(1)]
    public string? LanguageUnit { get; set; }

    [MaxLength(150), MinLength(1)]
    public string? Topic { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int ScoresTotal { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int PassedCount { get; set; }
}

public class CommonSection
{
    [Key]
    public int Id { get; set; }

    [Required, MinLength(1), MaxLength(500)]
    public string MarkdownPrompt { get; set; }

    [Required, Range(0, 1)]
    public int IsTestingQuestion { get; set; }

    [Required, Range(0, 1)]
    public int IsFreeAnswer { get; set; }

    [Required]
    public int StudyMaterialId { get; set; }

    public int? CorrectAnswerId { get; set; }

    [ForeignKey("StudyMaterialId")]
    public virtual StudyMaterial StudyMaterial { get; set; }

    [ForeignKey("CorrectAnswerId")]
    public virtual CorrectAnswer CorrectAnswer { get; set; }
}

public class PhoneticSection : IValidatableObject
{
    [Key]
    public int Id { get; set; }

    [Required, MinLength(1), MaxLength(500)]
    public string MarkdownPrompt { get; set; }

    [Required, MinLength(1), MaxLength(500)]
    public string GivenText { get; set; }

    [Required, MinLength(1), MaxLength(500)]
    public string UserAnswer { get; set; }

    [Required, Range(0, 1)]
    public int IsListeningAvailable { get; set; }

    [Required, Range(0, 1)]
    public int IsReadingAvailable { get; set; }

    [Required, Range(0, 1)]
    public int IsTextInputAvailable { get; set; }

    [Required, Range(0, 1)]
    public int IsVoiceInputAvailable { get; set; }

    [Required]
    public int StudyMaterialId { get; set; }

    [Required]
    public int CorrectAnswerId { get; set; }

    [ForeignKey("StudyMaterialId")]
    public virtual StudyMaterial StudyMaterial { get; set; }

    [ForeignKey("CorrectAnswerId")]
    public virtual CorrectAnswer CorrectAnswer { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (IsListeningAvailable <= 0 && IsReadingAvailable <= 0)
        {
            yield return new ValidationResult(
                "Либо переменная IsListeningAvailable должна быть > 0, либо переменная IsReadingAvailable.",
                new[] { nameof(IsListeningAvailable), nameof(IsReadingAvailable) });
        }

        if (IsTextInputAvailable <= 0 && IsVoiceInputAvailable <= 0)
        {
            yield return new ValidationResult(
                "Либо переменная IsTextInputAvailable должна быть > 0, либо переменная IsVoiceInputAvailable.",
                new[] { nameof(IsTextInputAvailable), nameof(IsVoiceInputAvailable) });
        }
    }
}

public class AnswerOption
{
    [Key]
    public int Id { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int Scores { get; set; }

    [Required, MinLength(1), MaxLength(500)]
    public string Text { get; set; }

    [Required]
    public int CommonSectionId { get; set; }

    [ForeignKey("CommonSectionId")]
    public virtual CommonSection CommonSection { get; set; }
}

public class CorrectAnswer
{
    [Key]
    public int Id { get; set; }

    [Required, MinLength(1), MaxLength(500)]
    public string Text { get; set; }
}

public class Rating
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StudyMaterialId { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Range(1, 10)]
    public int? Value { get; set; }

    [ForeignKey("StudyMaterialId")]
    public virtual StudyMaterial StudyMaterial { get; set; }

    [ForeignKey("UserProfileId")]
    public virtual UserProfile UserProfile { get; set; }
}
```

#Идеи_проектов #Идеи_проектов/Multiverbum