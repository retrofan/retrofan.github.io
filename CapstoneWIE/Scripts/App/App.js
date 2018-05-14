var app = angular.module('app', ['ngSanitize', 'ui.tinymce']);

app.controller('blogController', ['$scope', '$http', '$window', function ($scope, $http, $window) {
  $scope.blogPosts = [];
  $scope.newBlog = { Content: "", Tags: [] };
  $scope.newTag = {};
  $scope.tags = [];
  $scope.sortedBlogs = [];
  $scope.AllTags = [];
  $scope.reddit = [];
  $scope.numLimit = 800;
  $scope.alert = { content: {}, title: {} };

  $http.get('/api/BlogPosts').then(function (response) {
    $scope.blogPosts = response.data;
    $scope.sortedBlogs = response.data;
  });

  $http.get('/api/Tags').then(function (response) {
    $scope.AllTags = response.data;
  });

  $scope.submitDraft = function () {
    if ($scope.alert.title.status == false && $scope.alert.content.status == false) {
      $scope.newBlog.Tags.forEach(function (currentValue) {
        currentValue.Name = currentValue.Name.toUpperCase();
      });
      $http.put('/api/BlogPosts', $scope.newBlog).then(function () {
        if ($scope.newBlog.BlogState === 0) {
          bootbox.alert("Your post has been saved", function () {
            document.location.replace('/Blog/AuthorHome');
          });
        }
        if ($scope.newBlog.BlogState === 1) {
          bootbox.alert("Your post has been submitted", function () {
            document.location.replace('/Blog/AuthorHome');
          });
        }
      });
    }
  }

  $scope.setBlog = function (id) {
    $http.get('/api/BlogPosts/' + id).then(function (response) {
      $scope.newBlog = response.data;
    });
  }

  $scope.addBlog = function () {
    if ($scope.alert.title.status == false && $scope.alert.content.status == false) {
      $scope.newBlog.Tags.forEach(function (currentValue) {
        currentValue.Name = currentValue.Name.toUpperCase();
      });
      $http.post('/api/BlogPosts', $scope.newBlog).then(function () {
        if ($scope.newBlog.BlogState === 0) {
          bootbox.alert("Your post has been saved", function () {
            document.location.replace('/Blog/AuthorHome');
          });
        }
        else if ($scope.newBlog.BlogState === 1) {
          bootbox.alert("Your post has been submitted", function () {
            document.location.replace('/Blog/AuthorHome');
          });
        }
      });
    }
  };

  $scope.sortByTag = function (id) {
    $scope.sortedBlogs = [];
    $scope.blogPosts.forEach(function (currentValue) {
      var index = 0;
      currentValue.Tags.forEach(function (currentValue) {
        if (currentValue.Id == id) {
          index++;
        }
      });
      if (index > 0) {
        $scope.sortedBlogs.push(currentValue);
      }
    });
  }

  $scope.showAllPosts = function () {
    $scope.sortedBlogs = $scope.blogPosts;
  }

  $scope.validateFields = function (newBlog) {

    if (newBlog.Content == '') {
      $scope.alert.content.status = true;
      $scope.alert.content.message = "Please enter something in the content";
    }
    else if (newBlog.Content.length >= 3999) {
      $scope.alert.content.status = true;
      $scope.alert.content.message = "You have exceeded the maximum amount of characters!";
    }
    else
      $scope.alert.content.status = false;

    if (newBlog.Title == null || newBlog.Title === '') {
      $scope.alert.title.status = true;
      $scope.alert.title.message = "Please enter a title";
    }
    else if (newBlog.Title.length >= 254) {
      $scope.alert.title.status = true;
      $scope.alert.title.message = "You have exceeded the maximum amount of characters!";
    }
    else
      $scope.alert.title.status = false;
  }

  $scope.submit = function () {
    $scope.validateFields($scope.newBlog);
    $scope.newBlog.BlogState = 1;
  }

  $scope.setDraft = function () {
    $scope.validateFields($scope.newBlog);
    $scope.newBlog.BlogState = 0;
  }

  $scope.addTag = function () {
    var i = 0;
    $scope.newBlog.Tags.forEach(function (currentValue) {
      if (currentValue.Name.toUpperCase() == $scope.newTag.Name.toUpperCase()) {
        i++;
      }
    });
    if (i === 0) {
      $scope.newBlog.Tags.push($scope.newTag);
    }
    $scope.newTag = '';
  }

  $http.get('https://www.reddit.com/r/learnprogramming.json').then(function (response) {
    $scope.reddit = response.data;
  });

  $scope.tinymceOptions = {
    selector: 'textarea',
    height: 350,
    plugins: [
        'advlist autolink lists link image charmap print preview anchor',
        'searchreplace visualblocks code fullscreen',
        'insertdatetime media table contextmenu paste code'
    ],
    toolbar:
        'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
    content_css: [
        '//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
        '//www.tinymce.com/css/codepen.min.css'
    ]
  }
}]);

app.controller('blogDetails', ['$scope', '$http', function ($scope, $http) {
  $scope.init = function (model) {
    $scope.post = model;
  };
}
]);

app.controller('authorController', ['$scope', '$http', function ($scope, $http) {
  $scope.blogPosts = [];
  $scope.blogsDisplayed = [];
  $scope.editPost = {};
  $scope.id = '';
  $scope.blog = {};

  $http.get('/api/Id').then(function (response) {
    $scope.id = response.data;
    getPostsById();
  });

  function getPostsById() {
    $http.get('/api/Author/' + $scope.id).then(function (response) {
      $scope.blogPosts = response.data;
      $scope.blogsDisplayed = $scope.blogPosts;
    });
  }

  $scope.deletePost = function (blog) {
    $http.delete('/api/BlogPosts/' + blog.Id).then(function () {
      var index = $scope.blogPosts.indexOf(blog);
      $scope.blogPosts.splice(index, 1);
      bootbox.alert("Post Deleted");
    });
  }

  $scope.submitDraft = function (id) {
    $http.get('/api/BlogPosts/' + id).then(function (response) {
      $scope.blog = response.data;
      $scope.blog.Id = id;
      $scope.blog.BlogState = 1;
      $http.put('/api/BlogPosts', $scope.blog).then(function () {
        bootbox.alert("Post Submitted", function () {
          document.location.replace('/Blog/AuthorHome');
        });
      });
    });
  }

  $scope.sortByPending = function () {
    $scope.blogsDisplayed = [];
    $scope.blogPosts.forEach(function (currentValue) {
      if (currentValue.BlogState === 1) {
        $scope.blogsDisplayed.push(currentValue);
      }
    });
  }

  $scope.sortByDraft = function () {
    $scope.blogsDisplayed = [];
    $scope.blogPosts.forEach(function (currentValue) {
      if (currentValue.BlogState === 0) {
        $scope.blogsDisplayed.push(currentValue);
      }
    });
  }

  $scope.showAll = function () {
    $scope.blogsDisplayed = $scope.blogPosts;
  }

  $scope.sortByTag = function (id) {
    $scope.blogsDisplayed = [];
    $scope.blogPosts.forEach(function (currentValue) {
      var index = 0;
      currentValue.Tags.forEach(function (currentValue) {
        if (currentValue.Id === id) {
          index++;
        }
      });
      if (index > 0) {
        $scope.blogsDisplayed.push(currentValue);
      }
    });
  }
}
]);

app.controller('adminController', ['$scope', '$http', '$window', function ($scope, $http, $window) {
  $scope.blogPosts = {};
  $scope.blog = {};
  $scope.blogStatus = {};

  $http.get('/api/Admin').then(function (response) {
    $scope.blogPosts = response.data;
  });

  $scope.Approve = function (blog) {
    blog.BlogState = 2;
    $http.put('/api/BlogPosts', blog).then(function () {
      var index = $scope.blogPosts.indexOf(blog);
      $scope.blogPosts.splice(index, 1);
      bootbox.alert("Post Approved");
    });
  }

  $scope.Deny = function (blog) {
    blog.BlogState = 0;
    $http.put('/api/BlogPosts', blog).then(function () {
      var index = $scope.blogPosts.indexOf(blog);
      $scope.blogPosts.splice(index, 1);
      bootbox.alert("Post Denied");
    });
  }
}
]);

app.controller('pageController', ['$scope', '$http', '$window', function ($scope, $http, $window) {
  $scope.newPage = {};
  $scope.page = {};
  $scope.alert = { content: {}, title: {} };

  $scope.addPage = function () {
    $scope.validatePages($scope.newPage);
    if ($scope.alert.title.status == false && $scope.alert.content.status == false) {
      $http.post('/api/Pages', $scope.newPage).then(function () {
        bootbox.alert("Page Created", function () {
          document.location.replace('/Blog/ViewPendingPosts');
        });
      });
    }
  };

  $scope.validatePages = function (newBlog) {
    if (newBlog.Content == null || newBlog.Content == '') {
      $scope.alert.content.status = true;
      $scope.alert.content.message = "Please enter something in the content";
    }
    else if (newBlog.Content.length >= 3999) {
      $scope.alert.content.status = true;
      $scope.alert.content.message = "You have exceeded the maximum amount of characters!";
    }
    else
      $scope.alert.content.status = false;

    if (newBlog.Title == null || newBlog.Title === '') {
      $scope.alert.title.status = true;
      $scope.alert.title.message = "Please enter a title";
    }
    else if (newBlog.Title.length >= 254) {
      $scope.alert.title.status = true;
      $scope.alert.title.message = "You have exceeded the maximum amount of characters!";
    }
    else
      $scope.alert.title.status = false;
  }


  $scope.deletePage = function (id) {
    $scope.newPage.Id = id;
    $http.delete('/api/Pages/' + id).then(function () {
      bootbox.alert("Page Deleted", function () {
        document.location.replace('/Home/Index');
      });
    });
  }

  $scope.setPage = function (id) {
    $http.get('/api/Pages/' + id).then(function (response) {
      $scope.page = response.data;
    });
  }

  $scope.editPage = function () {
    $scope.validatePages($scope.page);
    if ($scope.alert.title.status == false && $scope.alert.content.status == false) {
      $http.put('/api/Pages', $scope.page).then(function () {
        bootbox.alert("Page Updated", function () {
          document.location.replace('/Page/PageTemplate/' + $scope.page.Id);
        });
      });
    }
  }

  $scope.tinymceOptions = {
    selector: 'textarea',
    height: 350,
    plugins: [
        'advlist autolink lists link image charmap print preview anchor',
        'searchreplace visualblocks code fullscreen',
        'insertdatetime media table contextmenu paste code'
    ],
    toolbar:
        'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
    content_css: [
        '//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
        '//www.tinymce.com/css/codepen.min.css'
    ]
  }
}
]);

app.controller('navbarController', ['$scope', '$http', function ($scope, $http) {
  $scope.pages = [];

  $http.get('/api/Pages').then(function (response) {
    $scope.pages = response.data;
  });
}]);

particlesJS.load('particleJs', '/particlesjs-config.json', function () {
  console.log('callback - particles.js config loaded');
});

particlesJS.load('particleJs2', '/particlesjs-config.json', function () {
  console.log('callback - particles.js config loaded');
});