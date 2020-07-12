pipeline {
    agent {
        label 'Linux'
    }
    parameters{
        string(name: 'CLIENT_XML_VER', defaultValue: '5_3_6', description: 'KalturaClient XML version')
    }
    environment{
        AWS_REGION="us-west-2"
        S3_CLIENT_LIBS_OTT_TSTYPES="s3://clientlibs-ott-tstypes/${CLIENT_XML_VER}/"
        BRANCH_NAME=getGitBranchName()
    }
    stages {
        stage('Checkout'){
            steps{
                cleanWs()
                checkout scm
                script {
                    currentBuild.displayName = "#${BUILD_NUMBER}: ${BRANCH_NAME}-${CLIENT_XML_VER}"
                    sh(label: 'Downloading ClientXML', script: 'aws s3 cp s3://clientlibs/${CLIENT_XML_VER}/kalturaxml/KalturaClient.xml ./KalturaClient.xml')

                }
            }
        }
        stage('Generate Types and ZIP'){
            steps{
                script {
                    sh(label: 'Generate TsTypes', script:'php exec.php -tott tstypes ./output')
                    sh(label: 'ZIP', script:'$zip -r tstypes.zip output/tstypes/')
                }
            }
        }
        stage('Upload to S3') {
            steps {
                sh(
                    label:'Upload ZIP to S3',
                    script:"aws s3 cp tstypes.zip ${S3_CLIENT_LIBS_OTT_TSTYPES}")
            }
        }
    }
}
def getGitBranchName() {
    echo scm.branches[0].name.replace("*/", "")
    return scm.branches[0].name.replace("*/", "")
}
def transpileVersion(version) {
    return version.replace("_", "-")
}
