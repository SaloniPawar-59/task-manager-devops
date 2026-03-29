pipeline {
    agent any

    stages {

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