// Regex Ajax validation mail
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
//Connexion
function Connexion() {
    var mdp = false;
    var mail = false;
    var valid = false;
    if ($("#mailUtilisateur").val() == "" && $("#mdpUtilisateur").val() == "") {
        $("#mailUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $("#mdpUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $("#Messagelogin").text("Saisir les champs pour vous inscrire.");
    }
    else if ($("#mdpUtilisateur").val() == "") {
        $("#mdpUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $('#Messagelogin').css('display', 'block');
        $("#Messagelogin").text("Saisir votre mot de passe");
    } else if ($("#mdpUtilisateur").val() <5) {
            $("#mdpUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messagelogin').css('display', 'block');
            $("#Messagelogin").text("Mot de passe de 5 caractères minimum");
    } else {
        mdp = true;
        $("#mdpUtilisateur").css('background-color', 'rgba(217, 255, 212, 0.835)');
    }



    if ($("#mailUtilisateur").val() == "") {
        $("#mailUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $('#Messagelogin').css('display', 'block');
        $("#Messagelogin").text("Un email est obligatoire");
    } else {
        mail = true;
        $("#mailUtilisateur").css('background-color', 'rgba(217, 255, 212, 0.835)');
    }
   if (!validateEmail($("#mailUtilisateur").val())) {
        $("#mailUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $('#Messagelogin').css('display', 'block');
        $("#Messagelogin").text("Votre Email doit etre correct");
    } else {
        valid = true;
        $("#mailUtilisateur").css('background-color', 'rgba(217, 255, 212, 0.835)');
    }
    if (mdp == true && mail == true&&valid==true){
        $.ajax({
            type: 'Post',
            data: {
                Email: $("#mailUtilisateur").val(),
                Password: $("#mdpUtilisateur").val(),
                rememberMe: $("#rememberMe").get(0).checked
            },
            url: '/Utilisateurs/AjaxLogin',
            success: function (msg) {
                    
               
                var ok = msg;
                if (msg == 1) {
                    location.reload();
                } else if (msg == 2) {
                    $("#Messagelogin").text("Il y a une erreur de saisie dans votre mot de passe ou votre mail");
                    $(".validmess").hide();
                } else {
                    $("#Messagelogin").text(msg);
                    $(".validmess").show();
                }
                
            },
            error: function (msg) {
                
                $("#Messagelogin").text(msg);
            }
        });
    }
}
function ActiverCompte() {
    $.ajax({
        type: 'Post',
        data: {
            Email: $("#mailUtilisateur").val(),
            Password: $("#mdpUtilisateur").val(),
     
        },
        url: '/Utilisateurs/AjaxLogin',
        success: function (msg) {
           
            $("#Messagelogin").text("Un Email a été envoyé sur votre messagerie ");
         
                $(".validmess").hide();
            

        },
        error: function (msg) {

            $("#Messagelogin").text(msg);
        }
    });
}
function Inscription() {
    // initialisation 
    // mail mdp repeatmdp pseudo auront pour valeur true quand les zones de textes sont valides
    var mail = false;
    var mdp = false;
    var repeatmdp=false;
    var pseudo = false;

    //Vérification des zones de textes vides
    if ($("#mail").val() == "" && $("#password").val() == "" && $("#repeatpassword").val() == "" && $("#pseudoUtilisateur").val() == "") {
        $("#mail").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $("#password").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $("#repeatpassword").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $("#pseudoUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
        $("#Messageinscription").text("Saisir les champs pour vous inscrire.");
    } else {


        //Vérification saisie du pseudo
        if ($("#pseudoUtilisateur").val() == "") {
            $("#pseudoUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messageinscription').css('display', 'block');
            $("#Messageinscription").text("Veuillez saisir votre pseudo.");
        } else if ($("#pseudoUtilisateur").val().length <5) {
            $("#pseudoUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messageinscription').css('display', 'block');
            $("#Messageinscription").text("Choisissez un pseudo au moins 5 caractères.");
        } else if ($("#pseudoUtilisateur").val().length > 32) {
            $("#pseudoUtilisateur").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messageinscription').css('display', 'block');
            $("#Messageinscription").text("Choisissez un pseudo avec moins de caractères.");
        } else {
            pseudo = true;
            $("#pseudoUtilisateur").css('background-color', 'rgba(217, 255, 212, 0.835)');
        }
         //Vérification saisie du mot de passe répété comparaison chaine vide ou valeur du mot de passe
        if ($("#repeatpassword").val() == "" || $("#password").val() != $("#repeatpassword").val()) {
            $("#repeatpassword").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messageinscription').css('display', 'block');
            $("#Messageinscription").text("Vous devez répéter le mot de passe");
        } else {
            repeatmdp = true;
            $("#repeatpassword").css('background-color', 'rgba(217, 255, 212, 0.835)');
        }
        //Vérification saisie du mot de passe
        if ($("#password").val() == "") {
            $("#password").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messageinscription').css('display', 'block');
            $("#Messageinscription").text("Un password est obligatoire");
        }
        else if ($("#password").val().length < 5) {
                $("#password").css('background-color', 'rgba(255, 213, 212, 0.835)');
                $('#Messageinscription').css('display', 'block');
                $("#Messageinscription").text("Un password de 5 caractères minimum est obligatoire");
        } else if ($("#password").val().length > 30) {
                $("#password").css('background-color', 'rgba(255, 213, 212, 0.835)');
                $('#Messageinscription').css('display', 'block');
                $("#Messageinscription").text("Votre mot de passe ne doit pas dépasser 30 caractères");
        } else {
                mdp = true;
                $("#password").css('background-color', 'rgba(217, 255, 212, 0.835)');
        }
        //Vérification saisie du mail
        if ($("#mail").val() == "") {
            $("#mail").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messageinscription').css('display', 'block');
            $("#Messageinscription").text("Veuillez saisir votre Email");
        }
        else if (!validateEmail($("#mail").val())) {
            $("#mail").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messageinscription').css('display', 'block');
            $("#Messageinscription").text("Veuillez saisir un email valide");
        } else if ($("#mail").val().length > 254) {
            $("#mail").css('background-color', 'rgba(255, 213, 212, 0.835)');
            $('#Messageinscription').css('display', 'block');
            $("#Messageinscription").text("Veuillez saisir un email plus court.");
        } else {
            mail = true;
            $("#mail").css('background-color', 'rgba(217, 255, 212, 0.835)');
        }

        //console.log(mail);
        //console.log(mdp);
        //console.log(repeatmdp);
        //console.log(pseudo);

        //requete ajax lancé si verification des zones de textes sont valides
        if (mail == true && mdp == true && repeatmdp == true && pseudo == true) {
            $.ajax({

                type: 'Post',
                //Donnée renseigné par l'utilisateur dans les parametre de la fonsction inscription
                data: {
                    Email: $("#mail").val(),
                    Pseudo: $("#pseudoUtilisateur").val(),
                    MotdePasse: $("#password").val()
                },
                url: '/Utilisateurs/AjaxInscription',
                success: function (html) {
                    alert("youpi");
                    $("#mail").val("");
                    $("#pseudoUtilisateur").val("");
                    $("#password").val("");
                    $("#repeatpassword").val("");
                    $('#Messageinscription').css('display', 'block');
                    $("#Messageinscription").text("Inscription réussie, un email de validation d'inscription a été envoyé à votre adresse email").hide(3000);
                    $("#Messageinscription").text("Valider votre compte à partir de votre boite mail.").show();
                    $('.div_inscription').hide(10000);
                    $('.seConnecter').show(10000);
                    //$(".div__inscription").hide();
                    //$(".seConnecter").show();
                },
                error: function (html) {
                    $("#Messageinscription").text("Erreur lors de votre inscription");
                    alert("Erreur lors de votre inscription");

                }
            });
        }
    }
}


//fonction upload ---publication index
function btnUpload() {

    var upload = document.getElementById("Upload");
    upload.addEventListener('click', function () {
        var filename = document.getElementById("file").Value;
        var src = "Content/medias/" + filename;
        var x = document.createElement("IMG");
        x.setAttribute("src", src);
    });
}