// Regex Ajax validation mail
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
//Connexion
function Connexion() {
    if ($("#mailUtilisateur").val() == "") {
        $('#Message_login').css('display', 'block');
        $("#Message_login").text("Un email est obligatoire");
    }
    else if (!validateEmail($("#mailUtilisateur").val())) {
        $('#Message_login').css('display', 'block');
        $("#Message_login").text("Votre Email doit etre correct");
    }
    else if ($("#mdpUtilisateur").val() == "") {
        $('#Message_login').css('display', 'block');
        $("#Message_login").text("Entrer un password");
    } else {
        $.ajax({
            type: 'Post',
            data: {
                Email: $("#mailUtilisateur").val(),
                Password: $("#mdpUtilisateur").val(),
                rememberMe: $("#rememberMe").get(0).checked
            },
            url: '/Utilisateurs/AjaxLogin',
            success: function () {
          
                    location.reload();
               
            },
            error: function () {
                $("#Message_login").text("erreur connexion");
            }
        });
    }
}

function Inscription() {
    // initialisation 
    // mail mdp repeatmdp pseudo auront pour valeur true quand les zones de textes sont valides
    var mail = false;
    var mdp = false;
    var repeatmdp=false;
    var pseudo = false;

    //Vérification des zones de textes vides
    if ($("#mail").val() == "" && $("#password").val() == "" && $("#repeat_password").val() == "" && $("#pseudoUtilisateur").val() == "") {
        $("#mail").css('background-color', 'red');
        $("#password").css('background-color', 'red');
        $("#repeat_password").css('background-color', 'red');
        $("#pseudoUtilisateur").css('background-color', 'red');
    } else {
         //Vérification saisie du mail
        if ($("#mail").val() == "") {
            $("#mail").css('background-color', 'red');
            $('#Message_inscription').css('display', 'block');
            $("#Message_inscription").text("Un email est obligatoire");
        }
        else if (!validateEmail($("#mail").val())) {
            $("#mail").css('background-color', 'red');
            $('#Message_inscription').css('display', 'block');
            $("#Message_inscription").text("Saisir un email valide");
        } else {
            mail = true;
            $("#mail").css('background-color', 'green');
        }
         //Vérification saisie du mot de passe
        if ($("#password").val().length < 5) {
            $("#password").css('background-color', 'red');
            $('#Message_inscription').css('display', 'block');
            $("#Message_inscription").text("Un password de 5 caractères minimum est obligatoire");
        } else {
            mdp = true;
            $("#password").css('background-color', 'green');
        }
         //Vérification saisie du mot de passe répété comparaison chaine vide ou valeur du mot de passe
        if ($("#repeat_password").val() == "" || $("#password").val() != $("#repeat_password").val()) {
            $("#repeat_password").css('background-color', 'red');
            $('#Message_inscription').css('display', 'block');
            $("#Message_inscription").text("Vous devez répéter le mot de passe");
        } else {
            repeatmdp = true;
            $("#repeat_password").css('background-color', 'green');
        }

         //Vérification saisie du pseudo
        if ($("#pseudoUtilisateur").val() == "") {
            $("#pseudoUtilisateur").css('background-color', 'red');
            $('#Message_inscription').css('display', 'block');
            $("#Message_inscription").text("Un pseudo est obligatoire");
        } else {
            pseudo = true;
            $("#pseudoUtilisateur").css('background-color', 'green');
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
                    $("#mail").val() = "";
                    $("#pseudoUtilisateur").val() = "";
                    $("#password").val() = "";
                    $("#repeat_password").val() = "";
                    $('#Message_inscription').css('display', 'block');
                    $("#Message_inscription").text("Inscription réussie");
                    //$(".div__inscription").hide();
                    //$(".seConnecter").show();
                },
                error: function (html) {

                    alert("Ce compte existe déjà");

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