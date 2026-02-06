# TODO — Autenticação (Login / Signup / JWT)

## Frontend (React)

* [ ] Criar estado para armazenar autenticação (token e dados do usuário)
* [ ] Implementar envio de dados do login para o backend (POST /login)
* [ ] Implementar envio de dados do signup para o backend (POST /signup)
* [ ] Receber token JWT na resposta do backend
* [ ] Salvar token (localStorage ou cookie)
* [ ] Criar função para enviar o token no header Authorization
* [ ] Criar componente de rota protegida (PrivateRoute)
* [ ] Redirecionar usuário não autenticado para /login
* [ ] Redirecionar usuário autenticado para página principal
* [ ] Implementar logout (remover token)

## Backend

* [ ] Criar rota POST /signup

* [ ] Validar dados recebidos

* [ ] Hash da senha (bcrypt ou equivalente)

* [ ] Salvar usuário no banco

* [ ] Criar rota POST /login

* [ ] Validar email e senha

* [ ] Gerar JWT com tempo de expiração

* [ ] Retornar token para o frontend

## Proteção de rotas

* [ ] Criar middleware para verificar JWT
* [ ] Ler header Authorization (Bearer token)
* [ ] Validar assinatura do token
* [ ] Liberar acesso apenas se válido

## Melhorias futuras (opcional)

* [ ] Refresh token
* [ ] Expiração automática de sessão
* [ ] Tratamento global de erros
* [ ] Loading states durante requisições
* [ ] Persistência do usuário após reload da página
