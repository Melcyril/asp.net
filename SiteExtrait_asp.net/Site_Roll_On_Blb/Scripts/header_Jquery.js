//Fonction qui active un lien apres un clic 

//Initialisation
function init() {
    $(".seConnecter,.div_inscription").hide();
    $(".divSkatepark").hide();
    $(".normalarticleFuturedernier").hide();

}

//Animation Sport

function sport() {

    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '0.2' }, 1000).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '0.2' }, 1000).html();
    jQuery('.trot').text('TROTTINNETTE').animate({ opacity: '0.2' }, 1000).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '0.2' }, 1000).html();
    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '0.5' }, 1000).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '0.5' }, 1000).html();
    jQuery('.trot').text('TROTTINNETTE').animate({ opacity: '0.5' }, 1000).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '0.5' }, 1000).html();  
}
function sport2() {

    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '1' }, 500).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '0' }, 500).html();
    jQuery('.trot').text('TROTTINNETTE').animate({ opacity: '0' },500).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '0' },500).html();
    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '0' }, 500).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '1' }, 500).html();
    jQuery('.trot').text('TROTTINNETTE').animate({ opacity: '0' }, 500).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '0' }, 500).html();
    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '0' }, 500).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '0' }, 500).html();
    jQuery('.trot').text('TROTTINNETTE').animate({ opacity: '1' }, 500).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '0' }, 500).html();
    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '0' }, 500).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '0' }, 500).html();
    jQuery('.trot').text('TROTTINNETTE').animate({ opacity: '0' }, 500).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '1' }, 500).html();

}
function sport1() {

    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '1' }, 1000).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '1' }, 1000).html();
    jQuery('.trot').text('TROTTINNETTE').animate({ opacity: '1' }, 1000).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '1' }, 1000).html();
}
// window load
//$(document).ready(function () {


    //Initialisation
    //chargement de page disparition du formulaire de connexion
    $(".seConnecter,.div_inscription").hide();
    $(".sousmenu").hide();
    //$(".lienSousMenub").hide();
    //$(".lienSousMenuc").hide();


    $(".logosport").hide();
    $(".div_headerLogo_1").hide();
    $(".logosport").show(1000);
    $(".div_headerLogo_1").show(200);
    $(".div_headerLogo_1").animate({ opacity: '0' }, 400);
    $(".div_headerLogo_1").animate({ opacity: '10' }, 400);
    $(".reduire").hide();
    

    //compteur
    var g = 0;
    var h = 0;


    // Inscription--->hide disparait fontion evenement click
    $(".newsrecherche .headerConnexion .inscription").click(function () {
        h = 0;
        g++;
        if (g % 2 == 1) {
            $("#mail").val("");
            $("#pseudoUtilisateur").val("");
            $("#password").val("");
            $("#repeatpassword").val("");
            $("#pseudoUtilisateur").val("");
            $(".LiensMenu_2").removeClass("active");
            $(".newsrecherche .headerConnexion .connect").removeClass("active1");
            $(".carousel").animate({ bottom: '450px', opacity: '0' }, 300);
            $(".carousel,.divSkatepark,.seConnecter,.sousmenu").hide("slow");
            $(".div_inscription").show("slow");
            $(this).addClass("active1");

        } else {
        
            $(this).removeClass("active1");
            $(".div_inscription").hide("slow");
            $(".carousel").show().animate({ top: '0px', opacity: '10' }, 300);
            if ($("header").width() > 800) {
                $("imagecarou").show();
                $(".imagecaroumini").hide();
            } else {
                $(".imagecaroumini").show().animate({ top: '0px', opacity: '10' }, 300);
                $(".imagecaroumini").height(150);
                $(".imagecarou").hide();
            }
        }

    });


    $(".newsrecherche .headerConnexion .connect").click(function () {
        g = 0;
        h++;
        if (h % 2 == 1) {
            $("#mailUtilisateur").val("");
            $("#mdpUtilisateur").val("");
            $(".LiensMenu_2").removeClass("active");
            $(".newsrecherche .headerConnexion .inscription").removeClass("active1");
            $(".carousel").animate({ bottom: '450px', opacity: '0' }, 300);
            $(".carousel,.divSkatepark,.div_inscription,.sousmenu").hide("slow");
            $(".newsrecherche .headerConnexion .connect").addClass("active1");
            $(".seConnecter").show("slow");
            $(this).addClass("active1");
        } else {
           $(this).removeClass("active1");
            $(".newsrecherche .headerConnexion .connect").removeClass("active1");
          
            $(".seConnecter").hide("slow");
            $(".carousel").show().animate({ top: '0px', opacity: '10' }, 300);
            if ($("header").width() > 800) {
                $("imagecarou").show();
                $(".imagecaroumini").hide();
            } else {
                $(".imagecaroumini").show().animate({ top: '0px', opacity: '10' }, 300);
                $(".imagecaroumini").height(150);
                $(".imagecarou").hide();
            }
        }
    });
    //fontion evenement hover Inscription
$(".newsrecherche .headerConnexion .connect,.newsrecherche .headerConnexion .inscription").mouseover(function () {
        
    //$(this).css({ "background-color": "rgb(154, 210, 236)", "border-radius": "3px", "color": "rgb(0, 89, 131)" });

    });
$(".newsrecherche .headerConnexion .connect,.newsrecherche .headerConnexion .inscription").mouseout(function () {

    //$(this).css({ "background-color": "#e7eafd", "border-radius": "3px", "color": "rgb(0, 176, 207)" });

});
    // Event click 
    $(".LiensMenu_2").click(function () {
        $(".newsrecherche .headerConnexion .connect,.newsrecherche .headerConnexion .inscription").removeClass("active1");
    });


    // L'association hover
    $(".LiensMenu_2b").hover(function () {

        $(".sousmenu1,.sousmenu2,.sousmenu3,.sousmenu4").hide();
      
    
        if ($("header").width() > 768) {
            $(".sousmenu0").show();
        } else {
            $(".sousmenu0").hide();
        }
    });
    // L'association  mouseleave
    $(".sousmenu0").mouseleave(function () {
        $(".sousmenu0").hide();
        $(".LiensMenu_2").removeClass("active");
    });
    // L'association click
    $(".LiensMenu_2b").click(function () {
        $(".sousmenu0,.sousmenu1,.sousmenu2,.sousmenu3,.sousmenu4,.sousmenuPort0,.sousmenuPort1,.sousmenuPort2,.sousmenuPort3,.sousmenuPort4").hide();
        if ($("header").width() < 768) {

            $(".sousmenuPort0").show();
            $(".sousmenu0").hide();
        } else {

                $(".sousmenu0").show();
                $(".sousmenuPort0").hide();
            
        }
    });

    //$(".headLogoMenu,.corp1").click(function () {
    //    $(".sousmenu0").hide();
    //});
    // Skatepark hover
    $(".LiensMenu_2c").hover(function () {
        $(".sousmenu0,.sousmenu2,.sousmenu3,.sousmenu4").hide();
  
        if ($("header").width() > 768) {
            $(".sousmenu1").show();
        } else {
            $(".sousmenu1").hide();
        }
    });
    // Skatepark mouseleave
    $(".sousmenu1").mouseleave(function () {
        $(".sousmenu1").hide();
        $(".LiensMenu_2").removeClass("active");
    });
    // Skatepark click
    $(".LiensMenu_2c").click(function () {
        $(".sousmenu0,.sousmenu1,.sousmenu2,.sousmenu3,.sousmenu4,.sousmenuPort0,.sousmenuPort2,.sousmenuPort3,.sousmenuPort4").hide();
        if ($("header").width() < 768) {

            $(".sousmenuPort1").show();
            $(".sousmenu1").hide();
        } else {

                $(".sousmenu1").show();
                $(".sousmenuPort1").hide();
            
            
        }
    });
    //Evenement hover
    $(".LiensMenu_2d").hover(function () {
        $(".sousmenu0,.sousmenu1,.sousmenu3,.sousmenu4").hide();
   
        if ($("header").width() > 768) {
            $(".sousmenu2").show();
        } else {
            $(".sousmenu2").hide();
        }
    });
    //Evenement mouseleave
    $(".sousmenu2").mouseleave(function () {
        $(".sousmenu2").hide();
        $(".LiensMenu_2").removeClass("active");
    });
    //Evenement click
$(".LiensMenu_2d").click(function () {
    $(".sousmenu0,.sousmenu1,.sousmenu2,.sousmenu3,.sousmenu4,.sousmenuPort0,.sousmenuPort1,.sousmenuPort3,sousmenuPort4").hide();
    if ($("header").width() < 768) {

        $(".sousmenuPort2").show();
        $(".sousmenu2").hide();
    } else {

            $(".sousmenu2").show();
            $(".sousmenuPort2").hide();
        

    }
});
    //Initiation et cour hover
    $(".LiensMenu_2e").hover(function () {

        $(".sousmenu0,.sousmenu1,.sousmenu2,.sousmenu4").hide();

        if ($("header").width() > 1023) {
            $(".sousmenu3").show();
        } else {
            $(".sousmenu3").hide();
        }
    });
    //Initiation et cour mouseleave
    $(".sousmenu3").mouseleave(function () {
        $(".sousmenu3").hide();
        $(".LiensMenu_2").removeClass("active");
    });
    //Initiation et cour click
    $(".LiensMenu_2e").click(function () {
        $(".sousmenu0,.sousmenu1,.sousmenu2,.sousmenu3,.sousmenu4,.sousmenuPort0,.sousmenuPort1,.sousmenuPort2,.sousmenuPort4").hide();
        if ($("header").width() < 768) {

            $(".sousmenuPort3").show();
            $(".sousmenu3").hide();
        } else {

                $(".sousmenu3").show();
                $(".sousmenuPort3").hide();
            

        }
        
    });
//admin hover
$(".LiensMenu_2f").hover(function () {

    $(".sousmenu0,.sousmenu1,.sousmenu2,.sousmenu3").hide();

    if ($("header").width() > 1023) {
        $(".sousmenu4").show();
    } else {
        $(".sousmenu4").hide();
    }
});
//Initiation et cour mouseleave
$(".sousmenu4").mouseleave(function () {
    $(".sousmenu4").hide();
    $(".LiensMenu_2").removeClass("active");
});
//Initiation et cour click
$(".LiensMenu_2f").click(function () {
    $(".sousmenu0,.sousmenu1,.sousmenu2,.sousmenu3,.sousmenu4,.sousmenuPort0,.sousmenuPort1,.sousmenuPort2,.sousmenuPort3").hide();
    if ($("header").width() < 768) {

        $(".sousmenuPort4").show();
        $(".sousmenu4").hide();
    } else {

        $(".sousmenu4").show();
        $(".sousmenuPort4").hide();
    }

});
//galerie mouseout
$("ul li .LiensMenu_22").mouseout(function () {

    $(".div_headerMenu_2 li").removeClass("active");
});
//galerie mouseout
$(".newsrecherche").hover(function () {

    $("ul li .LiensMenu_22").removeClass("active");
});
//galerie hover
$("ul li .LiensMenu_22").hover(function () {

    $(".sousmenu").hide();
});
//corp hover 
$(".body-content").hover(function () {
    $(".LiensMenu_2").removeClass("active");
    $(".sousmenu").hide();
});




    //Contact
    //$(".LiensMenu_2f").hover(function () {
    //    $(".sousmenu").hide();
    //});
    //$(".LiensMenu_2f").click(function () {
    //    $(".sousmenu").hide();
    //});

    //active le lien du menu cliqué
    $(".LiensMenu_2").hover(function () {
        $(".LiensMenu_2").removeClass("active");
        $(this).addClass("active");
        $(".connect,.inscription").removeClass("active1");
    });
//-----------------------------------------------------------agrandir reduire texte
    var a = 0;
    //Publication news texte complet
    $(".agrandir").click(function () {
        //var theId = $(this).attr('id');
        var id = this.id;
        var index = id.substring(8);
        $("#mini_article" + index).hide();
        $("#agrandir" + index).hide();
        $("#normal_article" + index).show();
        $("#reduire" + index).show();
    }); 
    //Publication news parti du texte 
    $(".reduire").click(function () {
        //var theId = $(this).attr('id');
        var id = this.id;
        var index = id.substring(7);
        $("#normal_article" + index).hide();
        $("#reduire" + index).hide();
        $("#mini_article" + index).show();
        $("#agrandir" + index).show();

    });
//event futur homr index parti du texte 
$(".agrandirFuturedernier").click(function () {
    //var theId = $(this).attr('id');
    var id = this.id;
    var index = id.substring(21);
    $("#miniarticleFuturedernier" + index).hide();
    $("#agrandirFuturedernier" + index).hide();
    $("#normalarticleFuturedernier" + index).show();
    $("#reduireFuturedernier" + index).show();
}); 
//event futur homr index parti du texte 
$(".reduireFuturedernier").click(function () {
    //var theId = $(this).attr('id');
    var id = this.id;
    var index = id.substring(20);
    $("#normalarticleFuturedernier" + index).hide();
    $("#reduireFuturedernier" + index).hide();
    $("#miniarticleFuturedernier" + index).show();
    $("#agrandirFuturedernier" + index).show();

});

$(".agrandirRealisedernier").click(function () {
    //var theId = $(this).attr('id');
    var id = this.id;
    var index = id.substring(22);
    $("#miniarticleRealisedernier" + index).hide();
    $("#agrandirRealisedernier" + index).hide();
    $("#normalarticleRealisedernier" + index).show();
    $("#reduireRealisedernier" + index).show();
    $(".divEvent")
});
//Publication news parti du texte 
$(".reduireRealisedernier").click(function () {
    //var theId = $(this).attr('id');
    var id = this.id;
    var index = id.substring(21);
    $("#normalarticleRealisedernier" + index).hide();
    $("#reduireRealisedernier" + index).hide();
    $("#miniarticleRealisedernier" + index).show();
    $("#agrandirRealisedernier" + index).show();
    $(".divEvent").css("transition", "width 10s");
});
//---------------------------------------------------------------lien connection inscription
//dans connexion-->lien affichant l'inscription
$(".lieninscription").click(function () {
    $(".seConnecter").hide();
    $(".div_inscription").show();
});
//dans connexion-->lien affichant l'inscription
$(".lienconnexion").click(function () {
    $(".div_inscription").hide();
    $(".seConnecter").show();
});
$(".retour").click(function () {
    $(".div_inscription").hide();
    $(".seConnecter").hide();
});
//-----------------------------------------------------------Galerie
//fait apparaitre la feuille modal
$(".imgmini").click(function () {
    var id = this.id;
    var index = id.substring(7);
    $("#divmodal" + index).show();
    $("#map").hide();
});
$(".close").click(function () {
    $("#map").show();
    $(".divmodal").hide();
});
//securite clic droit
$("img").hover(function () {
    document.oncontextmenu = new Function("return false");
});
$("img").mouseleave(function () {
    document.oncontextmenu = new Function("return true");
});
$("video").hover(function () {
    document.oncontextmenu = new Function("return false");
});
$("video").mouseleave(function () {
    document.oncontextmenu = new Function("return true");
});
    //retour haut de page
    $(".retourHP").click(function () {
        window.scrollTo(0, 0);
    });
var cpt01 = 0;
$(".btntoggle").click(function () {
    //var theId = $(this).attr('id');
    cpt01++;
    if (cpt01 % 2 == 1) {
        $(".div_headerLiensMenu_2").show(500);
  
        $(".div_headerLiensMenu_2").animate({ height: "25%" });
    } else {
        $(".div_headerLiensMenu_2").animate({ height: "0px" });
        $(".sousmenuPort0,.sousmenuPort1,.sousmenuPort2,.sousmenuPort3,.sousmenuPort4").hide(250);
        $(".LiensMenu_2").removeClass("active");
        $(".div_headerLiensMenu_2").hide(500);
    }

});
    init();
$(function () {
 
    sport();
    sport2();
    sport1();
    //window.setInterval(sport2, 20000);
    window.setInterval(sport, 15000);
    window.setInterval(logo,1200);
    window.setInterval(sport1, 10000);


    });

//});
