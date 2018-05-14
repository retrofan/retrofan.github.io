create procedure AddBlogPost(
	@Title nvarchar(255),
	@Content nvarchar(max),
	@PostDate datetime,
	@ApplicationUserId nvarchar(128),
	@Id int output
)
as
insert into BlogPost values (@Title, @Content, @PostDate, @ApplicationUserId)

set @Id = SCOPE_IDENTITY();