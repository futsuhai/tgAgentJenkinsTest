// Получаем элемент селекта
const select = document.getElementById('swagger-select')

// Устанавливаем в селект текущую локаль
select.value = navigator.language

// Определяем хендлер для изменения значения select
select.addEventListener('change', function () {

    // Отправляем запрос на сервер для получения json спецификации swagger
    fetch('/swagger/v1/swagger.json', {
        headers: {

            // Устанавливаем заголовок для локализации
            'Accept-Language': `${this.value}`
        },
    })
        // Транслируем ответ в json
        .then(response => response.json())
        .then(swaggerDoc => {

            // Если определен объект swagger ui
            if (window.ui) {

                // Меняем спецификацию на локализованную
                window.ui.specActions.updateJsonSpec(swaggerDoc);
            }

        })

        // Выводим сообщение об ошибке
        .catch(error => console.error('Ошибка при загрузке swagger: ', error))
})

