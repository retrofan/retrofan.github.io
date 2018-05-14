select tb.Tag_Id, t.Name from TagBlogpost as tb 
full outer join Tag as t 
on tb.Tag_Id = t.Id
where tb.BlogPost_Id is NULL


select * from BlogPost
full outer join TagBlogPost
on BlogPost.Id = TagBlogPost.BlogPost_Id
full outer join Tag
on TagBlogPost.Tag_Id = Tag.Id
where TagBlogPost.Tag_Id IS NULL


delete from blogpost where blogpost.Id = 18

select * from TagBlogPost

select * from tag

--delete from TagBlogPost where BlogPost_Id = 

delete Tag from TagBlogPost
right outer join Tag
on TagBlogPost.Tag_Id = Tag.Id
where BlogPost_Id is NULL

select * from TagBlogPost

select * from tag

select * from [page]