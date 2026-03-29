pipeline {
    agent any

    stages {

        stage('Checkout Code') {
            steps {
                git 'https://github.com/SaloniPawar-59/task-manager-devops.git'
            }
        }

        stage('Build Application') {
            steps {
                sh 'dotnet restore'
                sh 'dotnet build'
            }
        }

        stage('Run Tests') {
            steps {
                sh 'dotnet test || true'
            }
        }

    }
}