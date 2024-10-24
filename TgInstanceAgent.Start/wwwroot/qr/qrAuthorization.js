/**
 * Класс авторизации инстанса Telegram
 */
class QrAuthorization {
    
    // Объект для хранения элементов вкладки QR-кода
    qrCodeTab = {};
    
    // Объект для хранения элементов вкладки пароля
    passwordTab = {};
    
    // Объект для хранения элементов вкладки профиля
    profileTab = {};
    
    // Объект для хранения элементов панели оповещений
    alert = {};
    
    // Элемент для хранения токена
    token;
    
    // Элемент для хранения ID инстанса
    id;

    /**
     * Конструктор
     */
    constructor() {
        
        // Получаем элемент для хранения токена
        this.token = document.querySelector('#token');
        
        // Получаем элемент для хранения ID инстанса
        this.id = document.querySelector('#instance');
        
        // Получаем элемент панели оповещений
        this.alert.panel = document.querySelector('#alert');

        // Получаем объект для хранения элементов вкладки QR-кода
        this.qrCodeTab.tab = document.querySelector("#qrCodeTab");

        // Получаем заглушку QR-кода
        this.qrCodeTab.plugQrCode = document.querySelector(".plug_qr_code");

        // Получаем иконку Telegram
        this.qrCodeTab.iconTelegram = document.querySelector(".icon_telegram");

        // Получаем элемент изображения QR-кода
        this.qrCodeTab.imageQrCode = document.querySelector("#qr");

        // Получаем элемент кнопки сканирования QR-кода
        this.qrCodeTab.scanQrButton = document.querySelector("#scanQrButton");

        // Создаем объект загрузчика для кнопки сканирования QR-кода
        this.qrCodeTab.scanQrButtonLoader = new Loader(this.qrCodeTab.scanQrButton);

        // Создаем объект QR-кода для элемента изображения QR-кода
        this.qrCodeTab.qrCode = new QRCode(this.qrCodeTab.imageQrCode);

        // Получаем объект для хранения элементов вкладки пароля
        this.passwordTab.tab = document.querySelector("#passwordTab");

        // Получаем элемент поля ввода пароля
        this.passwordTab.password = document.querySelector("#password");

        // Получаем элемент подсказки для пароля
        this.passwordTab.hint = document.querySelector('#passwordTab label[for="password"]');

        // Получаем элемент кнопки авторизации
        this.passwordTab.authButton = document.querySelector("#authButton");

        // Создаем объект загрузчика для кнопки авторизации
        this.passwordTab.authButtonLoader = new Loader(this.passwordTab.authButton);

        // Получаем объект для хранения элементов вкладки профиля
        this.profileTab.tab = document.querySelector("#profileTab");
    }

    /**
     * Метод запускает авторизацию инстанса Telegram
     */
    startQrAuthorization() {
        
        // Привязываем метод на клик по кнопке сканирования QR-кода
        this.qrCodeTab.scanQrButton.addEventListener("click", () => this.getQrCode());

        // Привязываем метод на клик по кнопке авторизации
        this.passwordTab.authButton.addEventListener("click", () => this.processPassword());
    }

    /**
     * Метод запускает получение QR-кода
     */
    async getQrCode() {
        
        // Закрываем кнопку
        this.qrCodeTab.scanQrButtonLoader.setDisable();

        // Создаем объект подключения к серверу
        const hubConnection = new signalR
            .HubConnectionBuilder()
            .withUrl('https://localhost:7131/apiTg/Authenticate', { accessTokenFactory: () => this.token.value })
            .build();

        try {
            
            // Запускаем подключение к серверу
            await hubConnection.start();

            // Подписываемся на поток "Authenticate"
            await hubConnection.stream("Authenticate", this.id.value).subscribe({
                next: (item) => {
                    switch (item.type) {
                        case "QrCode":
                            
                            // Генерируем QR-код из полученного кода
                            this.qrCodeTab.qrCode.makeCode(item.code);
                            
                            // Переключаем контролы формы
                            this.toggleControls(true);
                            break;
                        case "Password":
                            
                            // Показываем вкладку пароля с подсказкой
                            this.showPassword(item.hint);
                            break;
                        case "Authenticated":
                            
                            // Показываем вкладку профиля с данными пользователя
                            this.showProfile(item.user);
                            break;
                    }
                },
                complete: () => {
                    
                    // Прячем вкладку QR-кода
                    this.qrCodeTab.tab.classList.add('hidden');

                    // Переключаем контролы формы
                    this.toggleControls(false);

                    // Открываем кнопку
                    this.qrCodeTab.scanQrButtonLoader.setEnable();

                    // Останавливаем подключение к серверу
                    hubConnection.stop();
                },
                error: (e) => {
                    
                    // Переключаем контролы формы
                    this.toggleControls(false);

                    // Открываем кнопку
                    this.qrCodeTab.scanQrButtonLoader.setEnable();

                    // Показываем ошибку
                    this.showError(e);

                    // Останавливаем подключение к серверу
                    hubConnection.stop();
                }
            });
        } catch (e) {
            
            // Показываем ошибку
            this.showError("Не удалось установить соединение");

            // Переключаем контролы формы
            this.toggleControls(false);

            // Открываем кнопку
            this.qrCodeTab.scanQrButtonLoader.setEnable();
        }
    }

    // Метод показывает вкладку пароля с подсказкой
    /**
     * Метод запускает получение QR-кода
     */
    showPassword(hint) {
        
        // Показываем вкладку пароля
        this.passwordTab.tab.classList.remove('hidden');

        // Если есть подсказка, то устанавливаем ее
        if (hint) this.passwordTab.hint.innerText = "Подсказка: " + hint;
    }

    // Метод обрабатывает пароль
    /**
     * Метод запускает получение QR-кода
     */
    async processPassword() {
        
        // Закрываем кнопку
        this.passwordTab.authButtonLoader.setDisable();

        // Отправляем запрос на сервер для проверки пароля
        const response = await fetch(`https://localhost:7131/apiTg/Authentication/CheckPassword/${this.id.value}`, {
            method: "POST",
            headers: {
                'Authorization': 'Bearer ' + this.token.value,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ password: this.passwordTab.password.value })
        });

        // Если ответ не OK, то показываем ошибку
        if (!response.ok) {
            this.passwordTab.authButtonLoader.setEnable();
            await this.showErrorFromResponse(response);
            return;
        }

        // Получаем данные пользователя
        const me = await fetch(`https://localhost:7131/apiTg/Users/GetMe/${this.id.value}`, {
            method: "GET",
            headers: {
                'Authorization': 'Bearer ' + this.token.value,
                'Accept': 'application/json',
            }
        });

        // Если ответ не OK, то показываем ошибку
        if (!me.ok) {
            this.passwordTab.authButtonLoader.setEnable();
            await this.showErrorFromResponse(response);
            return;
        }

        // Открываем кнопку
        this.passwordTab.authButtonLoader.setEnable();

        // Прячем вкладку пароля
        this.passwordTab.tab.classList.add('hidden');

        // Показываем вкладку профиля с данными пользователя
        this.showProfile(await me.json());
    }

    /**
     * Метод показывает вкладку профиля с данными пользователя
     * @param profile - данные профиля
     */
    showProfile(profile) {
        
        // Создаем элемент для ID пользователя
        const id = document.createElement('div');
        id.classList.add('userInfo');
        id.textContent = `ID: ${profile.id}`;
        this.profileTab.tab.appendChild(id);

        // Если есть имя пользователя, то создаем элемент для него
        if (profile.username) {
            const username = document.createElement('div');
            username.classList.add('userInfo');
            username.textContent = `Имя пользователя: @${profile.username}`;
            this.profileTab.tab.appendChild(username);
        }

        // Если есть имя, то создаем элемент для него
        if (profile.firstName) {
            const firstName = document.createElement('div');
            firstName.classList.add('userInfo');
            firstName.textContent = `Имя: ${profile.firstName}`;
            this.profileTab.tab.appendChild(firstName);
        }

        // Показываем вкладку профиля
        this.profileTab.tab.classList.remove('hidden');
    }

    /**
     * Метод переключает контролы формы
     * @param qrCodeReceived - флаг, получено ли изображение QR кода
     */
    toggleControls(qrCodeReceived) {
        
        // Если изображение QR-кода получено
        if (qrCodeReceived) {
            
            // Делаем активным изображение QR-кода
            this.qrCodeTab.imageQrCode.classList.remove("hidden");

            // Прячем заглушку QR-кода
            this.qrCodeTab.plugQrCode.classList.add("hidden");

            // Делаем активной иконку Whatsapp
            this.qrCodeTab.iconTelegram.classList.add("hidden");
        } else {
            
            // Прячем изображение QR-кода
            this.qrCodeTab.imageQrCode.classList.add("hidden");

            // Делаем активной заглушку QR-кода
            this.qrCodeTab.plugQrCode.classList.remove("hidden");

            // Делаем не активной иконку Whatsapp
            this.qrCodeTab.iconTelegram.classList.remove("hidden");
        }
    }

    /**
     * Метод запускает получение QR-кода
     * @param response - ответ сервера
     */
    async showErrorFromResponse(response) {
        const data = await response.json();
        if (data.errors.error) {
            this.showError(data.errors.error);
        } else {
            this.showError("Ошибка: " + response.statusCode);
        }
    }

    /**
     * Метод показывает ошибку
     * @param error - ошибка
     */
    showError(error) {
        
        // Очищаем таймаут оповещения
        if (this.alert.timeout) clearTimeout(this.alert.timeout);

        // Устанавливаем текст оповещения
        this.alert.panel.innerText = error;

        // Показываем панель оповещений
        this.alert.panel.classList.remove("hidden");

        // Устанавливаем таймаут для скрытия панели оповещений
        this.alert.timeout = setTimeout(() => {
            this.alert.panel.classList.add("hidden");
        }, 3000);
    }
}
