//Affichage image apres avoir choisit l'image
//Publication 6 images index de 0 à 5
//image
function imageHandler(e2) {
    var store = document.getElementById('image');
    var img = document.createElement("img");
    img.style.width = "10%";
    img.src = e2.target.result;
  
    store.appendChild(img);
}
function incompatible(e2) {
    var store = document.getElementById('image');
    store.innerHTML = "Fichier incompatible";
}
function videoHandler(e2) {
    var store = document.getElementById('image');
    store.innerHTML = "<video controls width='40%'><source src=" + e2.target.result + " type='video/mp4'></video>";
}

//image caroussel affichage resolution
function imageHandlerCaroussel(e2) {
    var store = document.getElementById('imageCaroussel');
    store.innerHTML = '<img width="1339px" height="400px" src="' + e2.target.result + '">';
}
function incompatibleCaroussel(e2) {
    var store = document.getElementById('imageCarousselIncompatible');

    store.innerHTML = "Fichier incompatible, choississez une extension jpg, gif ou png";
}
function videoHandlerCaroussel(e2) {
    var store = document.getElementById('imageCaroussel');

    store.innerHTML = "<video controls width='40%'><source src=" + e2.target.result + " type='video/mp4'></video>";
}
//image partenaire
function imageHandlerPartenaire(e2) {
    var store = document.getElementById('imagePartenaire');
    store.innerHTML = '<img width="211px" height="84px" src="' + e2.target.result + '">';
}
function incompatiblePartenaire(e2) {
    var store = document.getElementById('imagePartenaire');

    store.innerHTML = "Fichier incompatible";
}
//Galeries
function imageHandlerGaleries(e2) {
    var store = document.getElementById('imageGaleries');
    store.innerHTML = '<img width="40%" src="' + e2.target.result + '">';
}
function incompatibleGaleries(e2) {
    var store = document.getElementById('imageGaleries');

    store.innerHTML = "Fichier incompatible";
}
function videoHandlerGaleries(e2) {
    var store = document.getElementById('imageGaleries');

    store.innerHTML = "<video controls width='40%'><source src=" + e2.target.result + " type='video/mp4'></video>";
}
//event change de la valeur de l'input file
//event inputfile filepub
$("#filesPub").change(function (e) {

    for (var i = 0; i < e.target.files.length; i++) {
        $('#urlPublication').val($("#filesPub").val());
        var fr = new FileReader();
        var filename = e.target.files[i];

        var fichier = e.target.files[i].type;
        var tab = fichier.split('/');
        if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
            fr.onload = imageHandler;
        } else if (tab[tab.length - 1] == '3gp' || tab[tab.length - 1] == 'avi' || tab[tab.length - 1] == 'mov' || tab[tab.length - 1] == 'mp4') {
            fr.onload = videoHandler;
        } else {
            fr.onload = incompatible;
        }
   
        fr.readAsDataURL(filename);
    }
});
//event inputfile filepub1
$("#filesPub1").change(function (e) {
    $('#url1Publication').val($("#filesPub1").val());
    var fr = new FileReader();
    var filename = e.target.files[0];

    var fichier = e.target.files[0].type;
    var tab = fichier.split('/');
    if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
        fr.onload = imageHandler1;
    } else if (tab[tab.length - 1] == '3gp' || tab[tab.length - 1] == 'avi' || tab[tab.length - 1] == 'mov' || tab[tab.length - 1] == 'mp4') {
        fr.onload = videoHandler1;
    } else {
        fr.onload = incompatible1;
        
    }

    fr.readAsDataURL(filename);
});
//event inputfile filepub2
$("#filesPub2").change(function (e) {
    $('#url2Publication').val($("#filesPub2").val());
    var fr = new FileReader();
    var filename = e.target.files[0];

    var fichier = e.target.files[0].type;
    var tab = fichier.split('/');
    if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
        fr.onload = imageHandler2;
    } else if (tab[tab.length - 1] == '3gp' || tab[tab.length - 1] == 'avi' || tab[tab.length - 1] == 'mov' || tab[tab.length - 1] == 'mp4') {
        fr.onload = videoHandler2;
    } else {
        fr.onload = incompatible2;
    }

    fr.readAsDataURL(filename);
});
//event inputfile filepub3
$("#filesPub3").change(function (e) {
    $('#url3Publication').val($("#filesPub3").val());
    var fr = new FileReader();
    var filename = e.target.files[0];

    var fichier = e.target.files[0].type;
    var tab = fichier.split('/');
    if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
        fr.onload = imageHandler3;
    } else if (tab[tab.length - 1] == '3gp' || tab[tab.length - 1] == 'avi' || tab[tab.length - 1] == 'mov' || tab[tab.length - 1] == 'mp4') {
        fr.onload = videoHandler3;
    } else {
        fr.onload = incompatible3;
    }

    fr.readAsDataURL(filename);
});
//event inputfile filepub4
$("#filesPub4").change(function (e) {
    $('#url4Publication').val($("#filesPub4").val());
    var fr = new FileReader();
    var filename = e.target.files[0];

    var fichier = e.target.files[0].type;
    var tab = fichier.split('/');
    if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
        fr.onload = imageHandler4;
    } else if (tab[tab.length - 1] == '3gp' || tab[tab.length - 1] == 'avi' || tab[tab.length - 1] == 'mov' || tab[tab.length - 1] == 'mp4') {
        fr.onload = videoHandler4;
    } else {
        fr.onload = incompatible4;
    }

    fr.readAsDataURL(filename);
});
//event inputfile filepub5
$("#filesPub5").change(function (e) {
    $('#url5Publication').val($("#filesPub5").val());
    var fr = new FileReader();
    var filename = e.target.files[0];

    var fichier = e.target.files[0].type;
    var tab = fichier.split('/');
    if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
        fr.onload = imageHandler5;
    } else if (tab[tab.length - 1] == '3gp' || tab[tab.length - 1] == 'avi' || tab[tab.length - 1] == 'mov' || tab[tab.length - 1] == 'mp4') {
        fr.onload = videoHandler5;
    } else {
        fr.onload = incompatible5;
    }

    fr.readAsDataURL(filename);
});
//event inputfile filescaroussel
$("#filesCaroussel").change(function (e) {
    $('#urlCaroussel').val($("#filesCaroussel").val());
    var fr = new FileReader();
    var filename = e.target.files[0];

    var fichier = e.target.files[0].type;
    var tab = fichier.split('/');
    if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
        fr.onload = imageHandlerCaroussel;
    } else {
        fr.onload = incompatibleCaroussel;
    }

    fr.readAsDataURL(filename);
});
//event inputfile filespartenaire
$("#filesPartenaire").change(function (e) {
    $('#urlPartenaire').val($("#filesPartenaire").val());
    var fr = new FileReader();
    var filename = e.target.files[0];

    var fichier = e.target.files[0].type;
    var tab = fichier.split('/');
    if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
        fr.onload = imageHandlerPartenaire;
    } else {
        fr.onload = incompatiblePartenaire;
    }

    fr.readAsDataURL(filename);
});
//event inputfile filesgalerie
$("#filesGaleries").change(function (e) {
    $('#urlGalerie').val($("#filesGaleries").val());
    var fr = new FileReader();
    var filename = e.target.files[0];

    var fichier = e.target.files[0].type;
    var tab = fichier.split('/');
    if (tab[tab.length - 1] == 'jpg' || tab[tab.length - 1] == 'jpeg' || tab[tab.length - 1] == 'gif' || tab[tab.length - 1] == 'png') {
        fr.onload = imageHandlerGaleries;
    } else if (tab[tab.length - 1] == '3gp' || tab[tab.length - 1] == 'avi' || tab[tab.length - 1] == 'mov' || tab[tab.length - 1] == 'mp4') {
        fr.onload = videoHandlerGaleries;
    } else {
        fr.onload = incompatibleGaleries;
    }

    fr.readAsDataURL(filename);
});

//Evenement change de la liste ->cache la prevision d'venement quand on choisit news à la liste dans create publication
//$("#idTypePublication").change(function (e) {
//    i = this.selectedIndex;
//    if (i == 0) {
//        document.getElementById('datePrevisionPublication').style.display = 'none';
//        document.getElementById('labeldatePrevisionPublication').style.display = 'none';

//    } else {
//        document.getElementById('datePrevisionPublication').style.display = 'block';
//        document.getElementById('labeldatePrevisionPublication').style.display = 'block';
//    }
//});

function recherche() {
   var g=$("#TXTrecherche").val();
    $.ajax({

        type: 'Post',
        //Donnée renseigné par l'utilisateur dans les parametre de la fonsction inscription
        data: {
            recherche: $("#TXTrecherche").val(),
        },
        url: '/Recherche/Rechercher',
        success: function (html) {
            alert("snoupi");
         

        },
        error: function (html) {

            alert("Erreur de la recherche");

        }
    });
}
//function loadimage(e1) {
//    var filename = e1.target.files[0];
//    var fr = new FileReader();
//    fr.onload = imageHandler;
//    fr.readAsDataURL(filename);
//}
//function loadimage1(e1) {
//    var filename = e1.target.files[0];
//    var fr = new FileReader();
//    fr.onload = imageHandler1;
//    fr.readAsDataURL(filename);
//}
//function loadimage2(e1) {
//    var filename = e1.target.files[0];
//    var fr = new FileReader();
//    fr.onload = imageHandler2;
//    fr.readAsDataURL(filename);
//}
//function loadimage3(e1) {
//    var filename = e1.target.files[0];
//    var fr = new FileReader();
//    fr.onload = imageHandler3;
//    fr.readAsDataURL(filename);
//}
//function loadimage4(e1) {
//    var filename = e1.target.files[0];
//    var fr = new FileReader();
//    fr.onload = imageHandler4;
//    fr.readAsDataURL(filename);
//}
//function loadimage5(e1) {
//    var filename = e1.target.files[0];
//    var fr = new FileReader();
//    fr.onload = imageHandler5;
//    fr.readAsDataURL(filename);
//}

//window.onload = function () {
//    var url = document.getElementById("filesPub");
//    url.addEventListener('change', loadimage, false);
//    var url1 = document.getElementById("filesPub1");
//    url1.addEventListener('change', loadimage1, false);
//    var url2 = document.getElementById("filesPub2");
//    url2.addEventListener('change', loadimage2, false);
//    var url3 = document.getElementById("filesPub3");
//    url3.addEventListener('change', loadimage3, false);
//    var url4 = document.getElementById("filesPub4");
//    url4.addEventListener('change', loadimage4, false);
//    var url5 = document.getElementById("filesPub5");
//    url5.addEventListener('change', loadimage5, false);
//}

function supprimerpublication(idpublication) {
   
    $.ajax({

        type: 'Post',
        //Donnée renseigné par l'utilisateur dans les parametre de la fonsction inscription
        data: {
            id: idpublication,
        },
        url: '/Publications/_AjaxSupprimerPublication',
        success: function (html) {
            alert(html);
            $("#image_" + idpublication).fadeOut();

        },
        error: function (html) {

            alert("Erreur de la recherche");

        }
    });
}