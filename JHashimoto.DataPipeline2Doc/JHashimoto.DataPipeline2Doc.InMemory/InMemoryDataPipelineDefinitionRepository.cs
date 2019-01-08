using System;
using System.Collections.Generic;
using System.Text;
using JHashimoto.DataPipeline2Doc.Domain.DataPipelineDefinition;
using Codeplex.Data;

namespace JHashimoto.DataPipeline2Doc.InMemory {
    internal class InMemoryDataPipelineDefinitionRepository : IDataPipelineDefinitionRepository {

        private const string json = @"
{
    ""objects"": [
        {
            ""name"": ""Default"",
            ""myComment"": ""このオブジェクトは、パイプライン内のオブジェクトのデフォルト設定を定義するために使用されます。これらの値は、このJSONファイルを使用する全パイプラインで共通です。パイプライン個別の設定は、`parameters`セクションを参照してください。"",
            ""id"": ""Default"",
            ""failureAndRerunMode"": ""CASCADE"",
            ""pipelineLogUri"": ""#{myS3BaseUri}log/#{myPipelineName}"",
            ""scheduleType"": ""ONDEMAND"",
            ""maximumRetries"": ""#{myActivityMaximumRetries}"",
            ""myPipelineJsonVersion"": ""1.0.0.0"",
            ""resourceRole"": ""#{myResourceRole}"",
            ""role"": ""#{myRole}""
        },
        {
            ""name"": ""ActivityParent"",
            ""myComment"": ""全Activityの親オブジェクトです。全てのActivity共通の設定を定義するために使用されます。Activityを実行するEC2インスタンスを常時起動する場合はWorkerGroupを設定します。オンデマンドでインスタンスを生成する場合はrunsOnを設定します。"",
            ""id"": ""ActivityParent"",
            ""workerGroup"": ""#{myEc2WorkerGroup}"",
            ""onFail"" : {
                ""ref"": ""FailActionSnsAlarm""
            }
        },
        {
            ""name"": ""FailActionSnsAlarm"",
            ""myComment"": ""アクティビティ失敗時のアラームです。"",
            ""id"": ""FailActionSnsAlarm"",
            ""type"": ""SnsAlarm"",
            ""subject"": ""#{myErrorAlarmSubject}"",
            ""message"": ""#{myErrorAlarmMessage}"",
            ""topicArn"": ""#{myErrorAlarmTopicArn}""
        },
        {
            ""name"": ""RedshiftDatabase"",
            ""id"": ""RedshiftDatabase"",
            ""myComment"": ""Redshiftのデータベースです。"",
            ""type"": ""RedshiftDatabase"",
            ""connectionString"": ""jdbc:redshift://#{myRedshiftHost}:#{myRedshiftPort}/#{myRedshiftDatabase}"",
            ""username"": ""#{myRedshiftUsername}"",
            ""*password"": ""#{myRedshiftPassword}""
        },
        {
            ""name"": ""InsertSqlActivity"",
            ""id"": ""InsertImportParametersSqlActivity"",
            ""myComment"": ""登録します。"",
            ""type"": ""SqlActivity"",
            ""parent"": {
                ""ref"": ""ActivityParent""
            },
            ""database"": {
                ""ref"": ""RedshiftDatabase""
            },
            ""scriptUri"": ""#{myS3BaseUri}sql/#{myRedshiftSchema}/insert.sql"",
            ""scriptArgument"": [
                ""#{myActivatedDateTime}""
            ]
        },
        {
            ""name"" : ""完了S3KeyExists"",
            ""id"" : ""CompletedS3KeyExists"",
            ""type"" : ""S3KeyExists"",
            ""s3Key"" : ""#{myS3BaseUri}completion/completed.dat""
        }
    ],
    ""parameters"": [
        {
            ""id"": ""myPipelineName"",
            ""description"": ""このパイプラインの名前です。"",
            ""type"": ""String"",
            ""watermark"": ""パイプライン名""
        },
        {
            ""id"": ""myResourceRole"",
            ""description"": ""EC2インスタンスがアクセスできるリソースを制御するIAMロールです。"",
            ""type"": ""String"",
            ""watermark"": ""DataPipelineDefaultResourceRole"",
            ""default"": ""DataPipelineDefaultResourceRole""
        },
        {
            ""id"": ""myRole"",
            ""description"": ""AWS Data PipelineがEC2インスタンスを作成するために使用するIAMロールです。"",
            ""type"": ""String"",
            ""watermark"": ""DataPipelineDefaultRole"",
            ""default"": ""DataPipelineDefaultRole""
        },
        {
            ""id"": ""myS3BaseUri"",
            ""description"": ""S3のUriの共通部分です。"",
            ""type"": ""AWS::S3::ObjectKey"",
            ""watermark"": ""s3://bucketname/datapipeline/""
        },
        {
            ""id"": ""myRedshiftHost"",
            ""description"": ""Redshiftの接続先ホスト名です。"",
            ""type"": ""String"",
            ""watermark"": ""examplecluster.xxxxxxxxxxxx.ap-northeast-1.redshift.amazonaws.com""
        },
        {
            ""id"": ""myRedshiftPort"",
            ""description"": ""Redshiftの接続先ポートです。"",
            ""type"": ""Integer"",
            ""watermark"": ""5439"",
            ""default"": ""5439""
        },
        {
            ""id"": ""myRedshiftDatabase"",
            ""description"": ""Redshiftの接続先データベースです。"",
            ""type"": ""String"",
            ""watermark"": ""dev"",
            ""default"": ""dev""
        },
        {
            ""id"": ""myRedshiftSchema"",
            ""description"": ""Redshiftのスキーマ名です。"",
            ""type"": ""String"",
            ""watermark"": ""public"",
            ""default"": ""public""
        },
        {
            ""id"": ""myRedshiftUsername"",
            ""description"": ""Redshiftの接続ユーザー名です。"",
            ""type"": ""String"",
            ""watermark"": ""awsuser""
        },
        {
            ""id"": ""myRedshiftPassword"",
            ""description"": ""Redshiftの接続パスワードです。"",
            ""type"": ""String"",
            ""watermark"": ""password""
        },
        {
            ""id"": ""myErrorAlarmSubject"",
            ""description"": ""エラー通知アラームの件名です。"",
            ""type"": ""String"",
            ""watermark"": ""エラーメール""
        },
        {
            ""id"": ""myErrorAlarmMessage"",
            ""description"": ""エラー通知アラームの本文です。"",
            ""type"": ""String"",
            ""watermark"": ""エラーが発生しました。""
        },
        {
            ""id"": ""myErrorAlarmTopicArn"",
            ""description"": ""エラー通知アラームで使用するトピックのARNです。"",
            ""type"": ""String"",
            ""watermark"": ""arn:aws:sns:ap-northeast-1:XXXXXXXXXXXX:XXXXXX""
        },
        {
            ""id"": ""myActivityMaximumRetries"",
            ""description"": ""アクティビティ失敗時の最大再試行回数です。"",
            ""type"": ""Integer"",
            ""watermark"": ""2"",
            ""default"": ""2"",
            ""allowedValues"": [""0"", ""1"", ""2"", ""3"", ""4"", ""5""]
        },
        {
            ""id"": ""myEc2WorkerGroup"",
            ""description"": ""ワーカーグループです。既存のEC2インスタンスでアクティビティを実行する場合に指定します。開発環境でのみ使用します。"",
            ""type"": ""String"",
            ""watermark"": ""wg10000"",
            ""default"": ""wg10000"",
            ""optional"": ""true""
        },
        {
            ""id"": ""myActivatedDateTime"",
            ""description"": ""パイプラインをアクティブ化した日時です。実行時に取得します。"",
            ""type"": ""String"",
            ""watermark"": ""2018-01-01"",
            ""optional"": ""true""
        }
    ]
}
";

        public string FindById(string key) {
            return json;
        }

        public string GetObjectsName() {
            dynamic obj = DynamicJson.Parse(json);
            string value = obj.objects[0].name;

            return value;
            
        }
    }
}
