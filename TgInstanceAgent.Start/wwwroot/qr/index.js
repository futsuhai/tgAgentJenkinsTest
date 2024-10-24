(() => {

	//по загрузке страницы
	window.addEventListener("load", () => {

		//создаем инстанс класса авторизации
		const instanceAuthorization = new QrAuthorization();

		//запускаем авторизацию
		instanceAuthorization.startQrAuthorization();
	});

})();
