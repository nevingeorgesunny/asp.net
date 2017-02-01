var HomePageApp = angular.module('HomePageApp', ['ngRoute']);



 window.fbAsyncInit = function() {
        FB.init({
          appId      : '1034132449972714',
          xfbml      : true,
          version    : 'v2.4'
        });
      };

      (function(d, s, id){
         var js, fjs = d.getElementsByTagName(s)[0];
         if (d.getElementById(id)) {return;}
         js = d.createElement(s); js.id = id;
         js.src = "//connect.facebook.net/en_US/sdk.js";
         fjs.parentNode.insertBefore(js, fjs);
       }(document, 'script', 'facebook-jssdk'));








HomePageApp.controller('HomePageController',  ['$scope', '$http','$window', function ($scope, $http,$window){
    

    
    
   
    


        $scope.fbLogin = function() {
            
            FB.login(function(response){
                
               if(response.authResponse){
                   
                   console.log("wlecome");
                   
                   
                    FB.api('/me', function(response) {
                        console.log(response);
                        
                         $scope.fbLoginText = response.name;
                        
                        var accessToken = FB.getAuthResponse();
                        console.log(accessToken);
                        
                      });
               }else{
                   console.log('failed');
               }
            });

                   
            
            
            
         };
    
    
    
    
       $scope.fbLoginText='Login Via Fb';
    
       $scope.c = { FirstName: "First Name", 
                   LastName: "Last Name", Email: "Email",password:"Password",rePassword:"Re-Password" };
    
       $scope.user = { FirstName: "-", 
                   LastName: "-", Email: "-",password:"a",rePassword:"b" };
    
    
       $scope.cities = ['Delhi','Mumbai','Bangalore','Chennai'];
       $scope.expectedcompensations = ['10,000','20,000','30,000','40,000+'];
       $scope.customertypes = ['TENANT','LANDLORD'];
       $scope.jobroles = ['Business Analyst','Quality Analyst','Customer Support','HTML/CSS Web Developer','Core Engineering'];
       $scope.LocationHoverChecker=false;   
       $scope.signradio='./gfx/radiooff.svg';
       $scope.checkboximg='./gfx/unchecked.svg';
       $scope.radio='./gfx/radiooff.svg';
       $scope.check=false;
       $scope.loginchange=0; 
       $scope.clr='#000000';
       $scope.signtoggleclass = 'signtoggleclassoff';
       $scope.signupchange=0;  
    
    
       $scope.locationMouseIn = function() {
           $scope.locangle = 180;  
       };                  
       $scope.locationMouseOut = function() {
           $scope.locangle = 0;
       };
       $scope.jobroleMouseIn = function() {
           $scope.jobroleangle = 180;  
       };                  
       $scope.jobroleMouseOut = function() {
           $scope.jobroleangle = 0;
       };
       $scope.exMouseIn = function() {
           $scope.exangle = 180;  
       };                  
       $scope.exMouseOut = function() {
           $scope.exangle = 0;
       };
    
    
       $scope.signupchangefunc = function() {
           console.log('visual studio entry');
           
            if($scope.signupchange==0)
                $scope.signupchange=1; 
            else
                $scope.signupchange=0; 
           $scope.loginchange=0; 
       };
    
    
    
    
    
    
    
      $scope.userSubmit = function(user){
          
         
         
          
           var userObj = { "FirstName": user.FirstName, "LastName": 
                user.LastName, "Email": user.Email, "Password": user.password };
          
          
          
           $http({
                method: 'POST',
                url: '/Home/testPost',
                data: userObj,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function  (data, status, headers, config)  {
               
             console.log(data);
               
            });
          

          
          
          
          
          
          
      }
    
      
      
      
      
      
      
      $scope.loginchangefunc = function() {
            $scope.signupchange=0; 
          
          
            if($scope.loginchange==0)
                $scope.loginchange=1; 
            else
                $scope.loginchange=0; 
       };
       $scope.locfontclrchanger = function(test) {
           
            if($scope.check==false){
                $scope.clr='#ff0000';
                $scope.check=true;
            }
            else{
                $scope.clr='#000000';
                $scope.check=false;
            }
       };
    
      $scope.test = function() {
//          console.log('entry'); 
       };
    
    
      $scope.locsetSelected = function() {
       if ( this.locselected == 'locselected') {
            this.locselected = '';
            this.checkboximg = './gfx/unchecked.svg';
       }
       else{
            this.locselected = 'locselected';
            this.checkboximg = './gfx/checked.svg';
       }
     }
      
      $scope.exSelected = function() {
         if ($scope.lastSelected) {
             $scope.lastSelected.locselected = '';
             $scope.lastSelected.radio='./gfx/radiooff.svg';
         }
         this.radio='./gfx/radioon.svg'; 
         this.locselected = 'locselected';
         $scope.lastSelected = this;
      } 
      
      
      
      $scope.signtoggle = function() {
          
         if ($scope.signlastSelected) {
             $scope.signlastSelected.signtoggleclass = 'signtoggleclassoff';
             $scope.signlastSelected.signradio='./gfx/radiooff.svg';
         }
         this.signradio='./gfx/radioon.svg'; 
         this.signtoggleclass = 'signtoggleclasson';
         $scope.signlastSelected = this;
      }
   
      
}] );



//
//HomePageApp.config(["$routeProvider"  function($routeProvider){
//                    
//                    $routeProvider
//                        .when("/",{
//                            templateUrl : 'views/home/Index.cshtml',
//                            controller : 'HomePageController'
//                    })
//                         .otherwise("/",{
//                            templateUrl : 'views/home/Index.cshtml',
//                            controller : 'HomePageController'
//                    });
//                    
//}]);




HomePageApp.factory('facebookService', function($q) {
    return {
        getMyLastName: function() {
            var deferred = $q.defer();
            FB.api('/me', {
                fields: 'last_name'
            }, function(response) {
                if (!response || response.error) {
                    deferred.reject('Error occured');
                } else {
                    deferred.resolve(response);
                }
            });
            return deferred.promise;
        }
    }
});



HomePageApp.directive('exdowspin', function () {
   return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.degrees, function (rotateDegrees) {
               
                var r = 'rotate(' + rotateDegrees + 'deg)';
                element.css({
                    '-moz-transform': r,
                    '-webkit-transform': r,
                    '-o-transform': r,
                    '-ms-transform': r,
                    'margin-left': '16',
                    '-webkit-transition': '500ms ease all',
                    '-moz-transition': '500ms ease all',
                    '-o-transition': '500ms ease all',
                    'transition': '500ms ease all'
                });
            });
        }
    }
});



HomePageApp.directive('showsignup', function () {
   return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.implimenter, function (value) {
                console.log(value);
                if(value==0)
                    element.css({

                          'visibility':'hidden',

                    });
                else
                    element.css({

                          'visibility':'visible',

                    }); 
            });
        }
    }
});


HomePageApp.directive('showlogin', function () {
   return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.implimenter, function (value) {
                
                  console.log('message from logon window');
                
                 console.log(value);
                
                if(value == 0){
                    element.css({
                          'visibility': 'hidden',
//                          '-webkit-transition': '500ms ease all',
//                          '-moz-transition': '500ms ease all',
//                          '-o-transition': '500ms ease all',
//                          'transition': '500ms ease all'
                    });
                }
                  else{
                    element.css({
                          'visibility': 'visible',
//                          '-webkit-transition': '500ms ease all',
//                          '-moz-transition': '500ms ease all',
//                          '-o-transition': '500ms ease all',
//                          'transition': '500ms ease all'
                    });
                  }
            });
        }
    }
});


HomePageApp.directive('signchanger', function () {
   return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.implimenter, function (value) {
                if(value==1)
                   element.css({
                      'color':'#ffd93c',
                      '-webkit-transition': '500ms ease all',
                      '-moz-transition': '500ms ease all',
                      '-o-transition': '500ms ease all',
                      'transition': '500ms ease all'
                     });
                 if(value==0)
                   element.css({
                      'color':'#ffffff',
                      '-webkit-transition': '500ms ease all',
                      '-moz-transition': '500ms ease all',
                      '-o-transition': '500ms ease all',
                      'transition': '500ms ease all'
                     });
                
            });
        }
    }
});


HomePageApp.directive('logchanger', function () {
   return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.implimenter, function (value) {
                if(value==1)
                   element.css({
                      'color':'#ffd93c',
                      '-webkit-transition': '500ms ease all',
                      '-moz-transition': '500ms ease all',
                      '-o-transition': '500ms ease all',
                      'transition': '500ms ease all'
                     });
                 if(value==0)
                   element.css({
                      'color':'#ffffff',
                      '-webkit-transition': '500ms ease all',
                      '-moz-transition': '500ms ease all',
                      '-o-transition': '500ms ease all',
                      'transition': '500ms ease all'
                     });
                
            });
        }
    }
});



HomePageApp.directive('jobroledowspin', function () {
   return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.degrees, function (rotateDegrees) {
              
                var r = 'rotate(' + rotateDegrees + 'deg)';
                element.css({
                    '-moz-transform': r,
                    '-webkit-transform': r,
                    '-o-transform': r,
                    '-ms-transform': r,
                    'margin-left': '16',
                    '-webkit-transition': '500ms ease all',
                    '-moz-transition': '500ms ease all',
                    '-o-transition': '500ms ease all',
                    'transition': '500ms ease all'
                });
            });
        }
    }
});



HomePageApp.directive('locdowspin', function () {
   return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.degrees, function (rotateDegrees) {
              
                var r = 'rotate(' + rotateDegrees + 'deg)';
                element.css({
                    '-moz-transform': r,
                    '-webkit-transform': r,
                    '-o-transform': r,
                    '-ms-transform': r,
                    'margin-left': '16',
                    '-webkit-transition': '500ms ease all',
                    '-moz-transition': '500ms ease all',
                    '-o-transition': '500ms ease all',
                    'transition': '500ms ease all'
                });
            });
        }
    }
});



