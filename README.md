# 🚀 PretoBoost 2.0 - Windows Optimization Panel

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet" alt=".NET 8">
  <img src="https://img.shields.io/badge/WPF-Application-0078D4?style=for-the-badge&logo=windows" alt="WPF">
  <img src="https://img.shields.io/badge/License-MIT-green?style=for-the-badge" alt="License">
</p>

## 📋 Sobre

**PretoBoost 2.0** é um painel de otimização completo para Windows 10/11, desenvolvido em C# WPF com .NET 8. O aplicativo oferece uma interface moderna e intuitiva com tema escuro e detalhes em azul neon.

## ✨ Funcionalidades

### 🔧 Universal Boost
- Desativar Histórico de Acesso Rápido
- Desabilitar serviços de telemetria
- Desinstalar OneDrive
- Desativar Cortana
- Otimizações de privacidade
- Configurações de jogos (Game Mode, Xbox)
- E muito mais...

### 💻 Win10 Boost
- Ajustes de desempenho do sistema
- Desativar limitações de rede
- Otimizações de disco (Superfetch, Hibernação)
- Desabilitar telemetria de aplicativos
- Configurações de privacidade avançadas

### 🎮 GPU Boost
- **NVIDIA**: Tweaks de performance, P0-State, desabilitar telemetria
- **AMD**: Melhores configurações, priorização de GPU
- Configurações do GeForce Experience

### 🧹 Limpeza
- Arquivos temporários
- Logs do Windows/IIS
- Cache de Prefetch
- Minidumps de BSOD
- Lixeira
- Cache de media players

## 🛠️ Requisitos

- **Sistema Operacional**: Windows 10/11 (64-bit)
- **Runtime**: .NET 8.0 Desktop Runtime
- **Permissões**: Executar como Administrador

## 📦 Instalação

### Opção 1: Usando o executável pré-compilado
1. Baixe o arquivo `PretoBoost.exe` da seção Releases
2. Execute como Administrador

### Opção 2: Compilando do código fonte

#### Pré-requisitos
- Visual Studio 2022 (com workload de desenvolvimento .NET Desktop)
- .NET 8.0 SDK

#### Passos

1. Clone ou baixe o projeto
2. Abra o arquivo `PretoBoost.csproj` no Visual Studio 2022
3. Restaure os pacotes NuGet (automático)
4. Compile o projeto (F5 ou Ctrl+Shift+B)

#### Build via linha de comando

```powershell
# Navegue até a pasta do projeto
cd PretoBoost

# Restaurar dependências
dotnet restore

# Build em modo Debug
dotnet build

# Build em modo Release
dotnet build -c Release

# Publicar executável único (single-file)
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:EnableWindowsTargeting=true
```

O executável será gerado em:
```
bin\Release\net8.0-windows\win-x64\publish\PretoBoost.exe
```

## 📁 Estrutura do Projeto

```
PretoBoost/
├── Views/                    # Páginas XAML da interface
│   ├── MainWindow.xaml       # Janela principal
│   ├── UniversalBoostPage.xaml
│   ├── Win10BoostPage.xaml
│   ├── GpuBoostPage.xaml
│   └── CleaningPage.xaml
├── ViewModels/               # Lógica de apresentação (MVVM)
│   ├── MainViewModel.cs
│   ├── UniversalBoostViewModel.cs
│   ├── Win10BoostViewModel.cs
│   ├── GpuBoostViewModel.cs
│   └── CleaningViewModel.cs
├── Models/                   # Modelos de dados
│   └── ToggleAction.cs
├── Services/                 # Serviços de backend
│   ├── PowerShellService.cs
│   ├── RegistryService.cs
│   ├── ServiceManager.cs
│   ├── SystemTweaksService.cs
│   ├── Win10TweaksService.cs
│   ├── GpuTweaksService.cs
│   ├── CleaningService.cs
│   └── LogService.cs
├── Themes/                   # Estilos e temas
│   ├── DarkTheme.xaml
│   └── Controls.xaml
├── Logs/                     # Logs de alterações
│   └── tweaks.log
├── App.xaml                  # Configuração da aplicação
├── App.xaml.cs
├── PretoBoost.csproj         # Arquivo de projeto
└── app.manifest              # Manifest para elevação
```

## 📝 Logs

Todas as alterações são registradas em:
```
[DiretórioDoApp]\Logs\tweaks.log
```

Formato do log:
```
[2025-01-15 14:30:22] [SUCCESS] ✓ Telemetria desativada com sucesso
[2025-01-15 14:30:25] [REVERT] ↩ Telemetria revertida
[2025-01-15 14:30:30] [ERROR] ✗ Erro ao aplicar tweak: Acesso negado
```

## ⚠️ Avisos Importantes

1. **Execute como Administrador**: O aplicativo requer privilégios elevados para modificar configurações do sistema.

2. **Crie um ponto de restauração**: Antes de aplicar otimizações, especialmente as de GPU, crie um ponto de restauração do sistema.

3. **Use com responsabilidade**: Algumas otimizações podem afetar funcionalidades do Windows. Leia a descrição de cada opção antes de ativar.

4. **Compatibilidade**: Testado no Windows 10 22H2 e Windows 11 23H2.

## 🎨 Screenshots

### Interface Principal
- Tema escuro com detalhes em azul neon
- Menu lateral para navegação
- Cards com bordas arredondadas
- Toggle switches modernos com animações

## 🔄 Changelog

### v2.0.0
- Interface completamente redesenhada
- Suporte a .NET 8
- Novas otimizações de GPU (NVIDIA/AMD)
- Sistema de logging
- Botões "Aplicar Tudo" e "Reverter Tudo"
- Melhor organização em categorias

## 📄 Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo LICENSE para detalhes.

## 🤝 Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

---

**Desenvolvido com ❤️ para a comunidade Windows**
