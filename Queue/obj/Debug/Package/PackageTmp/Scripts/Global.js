function blockelement(el) {

    $(el).block(
        {
            message: '',
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff',
                top: ($(window).height() - 400) / 2 + 'px',
                left: ($(window).width() - 400) / 2 + 'px',
            }
        });
}

function block(name) {
    blockelement(name)
}  