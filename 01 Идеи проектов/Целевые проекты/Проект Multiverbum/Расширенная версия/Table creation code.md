
```sql
CREATE TABLE IF NOT EXISTS UserProfile (
    Id INTEGER PRIMARY KEY,
    Username TEXT NOT NULL CHECK(length(Username) <= 50 AND length(Username) > 1),
    Password TEXT NOT NULL UNIQUE CHECK(length(Password) <= 50 AND length(Password) > 2),
    StatusId INTEGER NOT NULL,
    TheoreticalMaterialsFinished INTEGER NOT NULL CHECK(TheoreticalMaterialsFinished >= 0),
    PracticalMaterialsFinished INTEGER NOT NULL CHECK(PracticalMaterialsFinished >= 0),
    PhoneticMaterialsFinished INTEGER NOT NULL CHECK(PhoneticMaterialsFinished >= 0),
    AverageScoresPractical INTEGER CHECK(AverageScoresPractical >= 0),
    AverageScoresPhonetic INTEGER CHECK(AverageScoresPhonetic >= 0),
    FOREIGN KEY (StatusId) REFERENCES Status(Id)
);

CREATE TABLE IF NOT EXISTS ScheduledMaterial (
    Id INTEGER PRIMARY KEY,
    StudyMaterialId INTEGER NOT NULL,
    UserProfileId INTEGER NOT NULL,
    PlannedDateTime DATETIME NOT NULL,
    IsExpired INTEGER NOT NULL CHECK(IsExpired >= 0),
    DoneInTime INTEGER CHECK(DoneInTime >= 0),
    FOREIGN KEY (StudyMaterialId) REFERENCES StudyMaterial(Id),
    FOREIGN KEY (UserProfileId) REFERENCES UserProfile(Id)
);

CREATE TABLE IF NOT EXISTS Status (
    Id INTEGER PRIMARY KEY,
    Name TEXT NOT NULL CHECK (Name IN ('admin', 'creator', 'student'))
);

CREATE TABLE IF NOT EXISTS Session (
    Id INTEGER PRIMARY KEY,
    WhenCreated DATETIME NOT NULL,
    Scores INTEGER NOT NULL CHECK(Scores >= 0),
    IsInterrupted INTEGER NOT NULL CHECK(IsInterrupted >= 0),
    StudyMaterialId INTEGER NOT NULL,
    UserProfileId INTEGER NOT NULL,
    LastSection INTEGER NOT NULL CHECK(LastSection > 0),
    FOREIGN KEY (StudyMaterialId) REFERENCES StudyMaterial(Id),
    FOREIGN KEY (UserProfileId) REFERENCES UserProfile(Id)
);

CREATE TABLE IF NOT EXISTS StudyMaterial (
    Id INTEGER PRIMARY KEY,
    CreatorId INTEGER NOT NULL,
    StudyMaterialTypeId INTEGER NOT NULL,
    Title TEXT NOT NULL,
    WhenCreated DATETIME NOT NULL,
    Description TEXT CHECK(length(Description) <= 500),
    LanguageUnit TEXT CHECK(length(LanguageUnit) <= 150),
    Topic TEXT CHECK(length(LanguageUnit) <= 150),
    ScoresTotal INTEGER NOT NULL CHECK(ScoresTotal >= 0),
    PassedCount INTEGER NOT NULL CHECK(PassedCount >= 0)
);

CREATE TABLE IF NOT EXISTS StudyMaterialType (
    Id INTEGER PRIMARY KEY,
    Type TEXT NOT NULL CHECK (Type IN ('theoretical', 'practical', 'phonetic'))
);

CREATE TABLE IF NOT EXISTS CommonSection (
    Id INTEGER PRIMARY KEY,
    MarkdownPrompt TEXT NOT NULL CHECK(length(MarkdownPrompt) >= 1 and lenght(MarkdownPrompt) <= 500),
    IsTestingQuestion INTEGER NOT NULL CHECK(IsTestingQuestion >= 0),
    IsFreeAnswer INTEGER NOT NULL CHECK(IsFreeAnswer >= 0),
    StudyMaterialId INTEGER NOT NULL,
    CorrectAnswerId INTEGER NULL,
    FOREIGN KEY (StudyMaterialId) REFERENCES StudyMaterial(Id),
    FOREIGN KEY (CorrectAnswerId) REFERENCES CorrectAnswer(Id)
);

CREATE TABLE IF NOT EXISTS PhoneticSection (
    Id INTEGER PRIMARY KEY,
    MarkdownPrompt TEXT NOT NULL CHECK(length(MarkdownPrompt) >= 1 and lenght(MarkdownPrompt) <= 500),
    GivenText TEXT NOT NULL CHECK(length(GivenText) >= 1 and length(GivenText) <= 500),
    UserAnswer TEXT NOT NULL CHECK(length(UserAnswer) >= 1 and length(UserAnswer) <= 500),
    IsListeningAvailable INTEGER NOT NULL CHECK(IsListeningAvailable >= 0),
    IsReadingAvailable INTEGER NOT NULL CHECK(IsReadingAvailable >= 0),
    IsTextInputAvailable INTEGER NOT NULL CHECK(IsTextInputAvailable >= 0),
    IsVoiceInputAvailable INTEGER NOT NULL CHECK(IsVoiceInputAvailable >= 0),
    StudyMaterialId INTEGER NOT NULL,
    CorrectAnswerId INTEGER NOT NULL,
    FOREIGN KEY (StudyMaterialId) REFERENCES StudyMaterial(Id),
    FOREIGN KEY (CorrectAnswerId) REFERENCES CorrectAnswer(Id),
    CHECK (IsListeningAvailable > 0 OR IsReadingAvailable > 0),
    CHECK (IsTextInputAvailable > 0 OR IsVoiceInputAvailable > 0)
);

CREATE TABLE IF NOT EXISTS AnswerOption (
    Id INTEGER PRIMARY KEY,
    Scores INTEGER NOT NULL CHECK(Scores >= 0),
    Text TEXT NOT NULL CHECK(length(Text) >= 1 and lenght(Text) <= 500),
    CommonSectionId INTEGER NOT NULL,
    FOREIGN KEY (CommonSectionId) REFERENCES CommonSection(Id)
);

CREATE TABLE IF NOT EXISTS CorrectAnswer (
    Id INTEGER PRIMARY KEY,
    Text TEXT NOT NULL CHECK(length(Text) >= 1 and lenght(Text) <= 500)
);

-- сочетание внешних ключей уникально
CREATE TABLE IF NOT EXISTS Rating (
    Id INTEGER PRIMARY KEY,
    StudyMaterialId INTEGER NOT NULL,
    UserProfileId INTEGER NOT NULL,
    Value INTEGER CHECK(Value > 0 AND Value <= 10),
    FOREIGN KEY (StudyMaterialId) REFERENCES StudyMaterial(Id),
    FOREIGN KEY (UserProfileId) REFERENCES UserProfile(Id),
    UNIQUE(StudyMaterialId, UserProfileId)
);

-- CHAT TABLES --

CREATE TABLE IF NOT EXISTS ChatMessage (
    Id INTEGER PRIMARY KEY,
    SenderId INTEGER NOT NULL,
    RecieverId INTEGER NOT NULL,
    Text TEXT CHECK(length(Text) >= 1 and lenght(Text) <= 500),
    TimeStamp DATETIME NOT NULL,
    FOREIGN KEY (SenderId) REFERENCES UserProfile(Id),
    FOREIGN KEY (RecieverId) REFERENCES UserProfile(Id),
);
```

#Идеи_проектов/Multiverbum