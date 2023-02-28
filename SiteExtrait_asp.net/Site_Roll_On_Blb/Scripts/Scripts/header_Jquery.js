//Fonction qui active un lien apres un clic 

//Initialisation
function init() {
    $(".seConnecter,.div_inscription").hide();
    $(".divSkatepark").hide();
}

//Animation Sport

function sport() {

    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '0.4' }, 20).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '0.4' }, 20).html();
    jQuery('.trot').text('TROTINNETTE').animate({ opacity: '0.4' }, 20).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '0.4' }, 20).html();
    jQuery('.skate').text('SKATEBOARD').animate({ opacity: '0.5' }, 20).html();
    jQuery('.bmx').text('BMX').animate({ opacity: '0.5' }, 20).html();
    jQuery('.trot').text('TROTINNETTE').animate({ opacity: '0.5' }, 20).html();
    jQuery('.roller').text('ROLLER').animate({ opacity: '0.5' }, 20).html();

    
}


// window load
//$(document).ready(function () {


    //Initialisation
    //chargement de page disparition du formulaire de connexion
    $(".seConnecter,.div_inscription").hide();
    $(".sousmenu").hide();
    //$(".lienSousMenub").hide();
    //$(".lienSousMenuc").hide();
    $(".p_art").hide();
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
            $(".div_LiensMenu_2").removeClass("active");
            //$(".newsrecherche .headerConnexion .connect").removeClass("active1");
            $(".carousel").animate({ bottom: '450px', opacity: '0' }, 300);
            $(".carousel,.divSkatepark,.seConnecter,.sousmenu").hide("slow");
            $(".div_inscription").show("slow");
            //$(this).addClass("active1");

            console.log(g);
        } else {
        
            //$(this).removeClass("active1");
            $(".div_inscription").hide("slow");
            $(".carousel").show().animate({ top: '0px', opacity: '10' }, 300);
            
            console.log(g);
        }

    });
    //fontion evenement clic de connexion--->carroussel disparait formulaire de connexion
    $(".newsrecherche .headerConnexion .connect").click(function () {
        g = 0;
        h++;
        if (h % 2 == 1) {
            $(".div_LiensMenu_2").removeClass("active");
            //$(".newsrecherche .headerConnexion .inscription").removeClass("active1");
            $(".carousel").animate({ bottom: '450px', opacity: '0' }, 300);
            $(".carousel,.divSkatepark,.div_inscription,.sousmenu").hide("slow");
            $(".seConnecter").show("slow");
            //$(".newsrecherche .headerConnexion .connect").addClass("active1");

            console.log(h);
        } else {
            if ($(".LiensMenu_2a").hasClass("active")) {
                //$(".newsrecherche .headerConnexion .connect").removeClass("active1");
                $(".seConnecter").hide("slow");
                $(".carousel").show().animate({ top: '0px', opacity: '10' }, 300);
            } else {
                //$(".newsrecherche .headerConnexion .connect").removeClass("active1");
                $(".seConnecter").hide("slow");
                $(".carousel").show().animate({ top: '0px', opacity: '10' }, 300);
            }
            console.log(h);
        }

    });

    // Event click 
    $(".div_LiensMenu_2").click(function () {
        $(".newsrecherche .headerConnexion .connect,.newsrecherche .headerConnexion .inscription").removeClass("active1");
    });

    //Fonction de redirection à la page index sur clic de accueil ou logo
    //$(".div_headerLogo_1,.LiensMenu_2a").click(function () {

    //    //window.location.href = "index.html";
    //});


    //Fonction fasaint le sous menu
    //i++ a chaque clic
    //association
    $(".LiensMenu_2a").hover(function () {

        $(".sousmenu,.seConnecter,.div_inscription").hide();
 
    });

    $(".LiensMenu_2b").hover(function () {

        $(".lienSousMenuc,.lienSousMenud,.lienSousMenue,.lienSousMenuf,.seConnecter,.div_inscription").hide();

    
        if ($(".corp1").width() > 1023) {
            $(".sousmenu,.lienSousMenub").show(500).css("left", "30%");
        } else {
            $(".sousmenu,.lienSousMenub").show(500);
        }
    });

    $(".LiensMenu_2b").click(function () {

        $(".lienSousMenuc,.lienSousMenud,.lienSousMenue,.lienSousMenuf,.seConnecter,.div_inscription").hide();
        $(".sousmenu,.lienSousMenub").show(500);
    });

    $(".headLogoMenu,.corp1").click(function () {
        $(".sousmenu").hide();
    });

    $(".LiensMenu_2c").hover(function () {
        $(".lienSousMenub,.lienSousMenud,.lienSousMenue,.seConnecter,div_inscription").hide();
  
        if ($(".corp1").width() > 1099) {
            $(".sousmenu,.lienSousMenuc").show(500).css("left", "42%");
        } else {
            $(".sousmenu,.lienSousMenuc").show(500);
        }
    });

    $(".LiensMenu_2c").click(function () {
        $(".lienSousMenub,.lienSousMenud,.lienSousMenue,.seConnecter,div_inscription").hide();
        $(".sousmenu,.lienSousMenuc").show(200);
    });
    //Evenement
    $(".LiensMenu_2d").hover(function () {
        $(".div_LiensMenu_2").removeClass("active");
        $(this).addClass("active");
        $(".lienSousMenub,.lienSousMenuc,.lienSousMenue,.lienSousMenuf,.seConnecter,.div_inscription").hide();
   
        if ($(".corp1").width() > 1023) {
            $(".sousmenu,.lienSousMenud").show(500).css("left", "57%");
        } else {
            $(".sousmenu,.lienSousMenud").show(500);
        }
    });
    $(".LiensMenu_2d").click(function () {

        $(".lienSousMenub,.lienSousMenuc,.lienSousMenue,.lienSousMenuf,.seConnecter,.div_inscription").hide();
        $(".sousmenu,.lienSousMenud").show(200);
    
    })
    //Initiation et cour
    $(".LiensMenu_2e").hover(function () {

        $(".lienSousMenud,.lienSousMenub,.lienSousMenuc,.lienSousMenuf,.seConnecter,.div_inscription").hide();

        if ($(".corp1").width() > 1023) {
            $(".sousmenu,.lienSousMenue").show(500).css("left", "70%");
        } else {
            $(".sousmenu,.lienSousMenue").show(500);
        }
    });
    $(".LiensMenu_2e").click(function () {
        $(".lienSousMenud,.lienSousMenub,.lienSousMenuc,.lienSousMenuf,.seConnecter,.div_inscription").hide();
        $(".sousmenu,.lienSousMenue").show(200);
    });
    //Contact
    $(".LiensMenu_2f").hover(function () {
        $(".sousmenu").hide();
    });
    $(".LiensMenu_2f").click(function () {
        $(".sousmenu").hide();
    });

    //active le lien du menu cliqué
    $(".div_LiensMenu_2").hover(function () {
        $(".div_LiensMenu_2").removeClass("active");
        $(this).addClass("active");
        $(".connect,.inscription").removeClass("active1");
    });

    var a = 0;
    //Publication texte complet
    $(".agrandir").click(function () {
        //var theId = $(this).attr('id');
        var id = this.id;
        var index = id.substring(8);
        $("#mini_article" + index).hide();
        $("#agrandir" + index).hide();
        $("#normal_article" + index).show();
        $("#reduire" + index).show();
    }); 
    //Publication parti du texte 
    $(".reduire").click(function () {
        //var theId = $(this).attr('id');
        var id = this.id;
        var index = id.substring(7);
        $("#normal_article" + index).hide();
        $("#reduire" + index).hide();
        $("#mini_article" + index).show();
        $("#agrandir" + index).show();

    });
    //retour haut de page
    $(".retourHP").click(function () {
        //var theId = $(this).attr('id');
        window.scrollTo(0, 0);

    });

    init();
    $(function () {
        sport();

        window.setInterval(sport, 0);
    });

//});
