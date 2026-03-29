pipeline {
    agent any

    environment {
        ECR_REPO = "304686171763.dkr.ecr.ap-south-1.amazonaws.com/task-manager-api"
    }

    stages {

        stage('Build Application') {
            steps {
                sh 'dotnet restore'
                sh 'dotnet build --no-restore'
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t task-manager-api .'
            }
        }

        stage('Tag Docker Image') {
            steps {
                sh 'docker tag task-manager-api:latest $ECR_REPO:latest'
            }
        }

        stage('Push to ECR') {
            steps {
                sh 'docker push $ECR_REPO:latest'
            }
        }

    }
}