var divcontacten;



function Swalsuccess() {
    swal("Success!", "succesvolle operatie", "success");
}


function getsizes() {


}

function iframeLoaded(iframe) {

    //var $currentIFrame = $('#uploadIFrame');
    //$currentIFrame.contents().find("body #height").val("Value from parent file.");



    //var iFrameID = document.getElementById('iframeContact');
    //console.log("************************************" + iframe)
    //if (iFrameID) {        
    //    iFrameID.height = "";
    //    iFrameID.height = iFrameID.contentWindow.document.body.scrollHeight + "px";
    //    console.log(iFrameID.height)
    //}
}




window.addEventListener('message', function (e) {
    var $iframe = jQuery("#myIframe");
    var eventName = e.data[0];
    var data = e.data[1];
    switch (eventName) {
        case 'setHeight':
            $iframe.height(data);
            break;
    }
}, false);