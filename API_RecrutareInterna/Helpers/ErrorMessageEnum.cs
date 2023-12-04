namespace API_RecrutareInterna.Helpers
{
    public class ErrorMessageEnum
    {
        public class Job
        {
            public const string NotFound = "Niciun job găsit în tabel!";
            public const string NotFoundById = "Job-ul cu ID-ul furnizat nu există";
            public const string BadRequest = "Formatul transmis nu este corect.";
            public const string ZeroUpdatesToSave = "Nu există actualizări de salvat pentru job.";
            public const string StartEndDateError = "Data de sfârșit nu poate fi anterioară datei de început.";
            public const string PozitieExistsError = "Pozitia deja există.";
        }
    }
}
