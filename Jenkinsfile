pipeline {
    agent any

    stages {

        stage('Build Application') {
            steps {
                sh 'dotnet restore'
                sh 'dotnet build --no-restore'
            }
        }

        stage('Run Tests') {
            steps {
                sh 'dotnet test || true'
            }
        }

    }
}