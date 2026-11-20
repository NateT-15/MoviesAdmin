CREATE TABLE [AudienceRating] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AudienceRating] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Genre] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Genre] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [UserSnapshot] (
    [UserId] nvarchar(450) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UserSnapshot] PRIMARY KEY ([UserId])
);
GO


CREATE TABLE [Movie] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [GenreId] int NOT NULL,
    [AudienceRatingId] int NOT NULL,
    [RuntimeMinutes] int NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [ReleaseDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Movie_AudienceRating_AudienceRatingId] FOREIGN KEY ([AudienceRatingId]) REFERENCES [AudienceRating] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Movie_Genre_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [CriticReview] (
    [Id] int NOT NULL IDENTITY,
    [MovieId] int NOT NULL,
    [Description] nvarchar(1000) NOT NULL,
    [Rating] int NOT NULL,
    [IsPublished] bit NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_CriticReview] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CriticReview_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_CriticReview_MovieId] ON [CriticReview] ([MovieId]);
GO


CREATE INDEX [IX_Movie_AudienceRatingId] ON [Movie] ([AudienceRatingId]);
GO


CREATE INDEX [IX_Movie_GenreId] ON [Movie] ([GenreId]);
GO


