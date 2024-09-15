# EmailSenderAPI
API на платформе .NET для
отправки текстовых сообщений на электронную почту, который позволяет отправлять сообщения как на
один, так и на несколько email-адресов, с логгированием в базу данных MongoDB.

## API Documentation
Endpoints:

`POST /EmailSender/send` - принимает EmailDto и отправляет сообщение на адреса указанные в нем.

```yml
  emailSubject:	string,
  emailMessage:	string,
  addressesList: [string]
```
Возвращает код 200 при успешной отправке на все указанные адреса. \
Возвращает код 409 при возникновении ошибки при отправке на один из адресов с сообщением ошибки, полученным от smtp.


`GET /EmailSender/get-logs` - возвращет все залоггированные сообщения в виде:
```yml
[
    id: int
    emailSubject: string
    message: string
    address: string
    sentTime: DateTime
    status: string
]
```

## Running API
API и база данных MongoDB легко запускаются с помощью docker compose.
В файл `docker-compose.yml` нужно добавить информацию о вашем аккаунте для smtp.
```yml
services:
  emailsenderapi:
    image: emailsenderapi
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__EmailsLogs=mongodb://user:pass@mongodb
      - MailSettings__Address= ...
      - MailSettings__ApplicationPassword= ...
      - MailSettings__SenderName= ...
```
`MailSettings__Address` - адрес почты \
`MailSettings__ApplicationPassword` - пароль от почты(пароль приложения для gmail например) \
`MailSettings__SenderName` - имя отправителя

Далее командой `docker compose up` поднимаются оба сервиса, API на порте 5135 и база данных MongoDB на порте 27018
