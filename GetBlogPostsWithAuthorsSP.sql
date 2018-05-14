create procedure GetBlogPostsWithAuthors as

select bp.*, anu.*
from BlogPost as bp
	inner join AspNetUsers as anu
	on bp.ApplicationUserId = anu.Id