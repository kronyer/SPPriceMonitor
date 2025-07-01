window.startBarcodeScanner = (dotNetHelper) => {
    if (!window.Quagga) {
        alert("QuaggaJS not loaded.");
        return;
    }
    Quagga.init({
        inputStream: {
            name: "Live",
            type: "LiveStream",
            target: document.querySelector('#camera'),
            constraints: {
                facingMode: "environment"
            }
        },
        decoder: {
            readers: ["ean_reader", "code_128_reader"]
        }
    }, function (err) {
        if (err) {
            console.error(err);
            return;
        }
        Quagga.start();
    });

    Quagga.onDetected(function (data) {
        Quagga.stop();
        dotNetHelper.invokeMethodAsync('OnBarcodeRead', data.codeResult.code);
    });
};

window.stopBarcodeScanner = () => {
    if (window.Quagga) {
        Quagga.stop();
    }
};