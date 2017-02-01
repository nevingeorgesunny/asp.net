HomePageApp.config(["$routeProvider",function($routeProvider){
    
    
   

    $routeProvider
        .when("/",{
            templateUrl : 'Views/Home/index.cshtml',
            controller : 'HomePageController'
        })
        .when("/Home/testPost",{
            templateUrl : 'Views/Home/testPost.cshtml',
            controller : 'HomePageController',
            authenticated : true
        })
        .otherwise('/',{
            templateUrl : 'Views/Home/index.cshtml',
            controller : 'HomePageController'
        });
        
}]);


HomePageApp.run(["$rootScope","$location","authFact",function($rootScope,$location,authFact){
    
     console.log("#######################");
    
   
    
    
    
    $rootScope.$on('$routeChangeStart' , function(event,next,current) {
        
        
        console.log("&&&&&&&&&&&&&&&&&&&&&");
        
        console.log(event);
        console.log(next);
        console.log(current);
        
    });
}]);