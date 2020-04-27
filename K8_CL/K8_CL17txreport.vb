Imports System.Collections.ObjectModel
Imports System.IO

Public Class K8_CL17txreport

    Private _TestRun As New TRXtestrun
    Private _Times As New TRXtimes
    Private _Result As New TRXresults

    Public Property TestRun As TRXtestrun
        Get
            Return _TestRun
        End Get
        Set(value As TRXtestrun)
            _TestRun = value
        End Set
    End Property

    Public Property Times As TRXtimes
        Get
            Return _Times
        End Get
        Set(value As TRXtimes)
            _Times = value
        End Set
    End Property

    Public Property Result As TRXresults
        Get
            Return _Result
        End Get
        Set(value As TRXresults)
            _Result = value
        End Set
    End Property


    Public Shared Sub LoadXMLIntoCollection()

        KiebitzAllAnalysis.Clear()

        If File.Exists("testresult1.txt") = True Then

            Dim document As XDocument = XDocument.Load("testresult1.txt")

            Dim TRXcomplete As IEnumerable(Of XElement) = document.Root.Elements()
            For Each RootNode As XElement In TRXcomplete
                'This is very sensitive on Upper/Lower case of the attributes !!!!!!!!

                If RootNode.Name.LocalName = "TestRun" Then
                    MyTRXreporter.TestRun.Id = RootNode.Attribute("id").Value
                    MyTRXreporter.TestRun.Name = RootNode.Attribute("name").Value
                    MyTRXreporter.TestRun.RunUser = RootNode.Attribute("runUser").Value
                ElseIf RootNode.Name.LocalName = "Times" Then
                    MyTRXreporter.Times.Creation = CDate(RootNode.Attribute("creation").Value)
                    MyTRXreporter.Times.Queuing = CDate(RootNode.Attribute("queuing").Value)
                    MyTRXreporter.Times.Start = CDate(RootNode.Attribute("start").Value)
                    MyTRXreporter.Times.Finish = CDate(RootNode.Attribute("finish").Value)
                ElseIf RootNode.Name.LocalName = "Results" Then
                        For Each UnitTestResultItem As XElement In RootNode.Elements '  .Element("UnitTestResult").Elements

                        MyTRXreporter.Result.UnitTestResult.Add(New TRXUnitTestResult With {
                        .ExecutionId = UnitTestResultItem.Attribute("executionId").Value,
                        .TestId = UnitTestResultItem.Attribute("testId").Value,
                        .TestName = UnitTestResultItem.Attribute("testName").Value,
                        .ComputerName = UnitTestResultItem.Attribute("computerName").Value,
                        .Duration = TimeSpan.Parse(UnitTestResultItem.Attribute("duration").Value),
                        .StartTime = CDate(UnitTestResultItem.Attribute("startTime").Value),
                        .EndTime = CDate(UnitTestResultItem.Attribute("endTime").Value),
                        .TestType = UnitTestResultItem.Attribute("testType").Value,
                        .Outcome = UnitTestResultItem.Attribute("outcome").Value,
                        .TestListID = UnitTestResultItem.Attribute("testListId").Value,
                        .RelativeResultsDirectory = UnitTestResultItem.Attribute("relativeResultsDirectory").Value
                        })

                    Next
                End If
            Next

        End If
    End Sub

    '<?xml version="1.0" encoding="utf-8"?>
    '<TestRun id = "748bcec8-4917-4274-896d-efb8dd3c8c49" name="mrank@HPELITEONE1000 2020-04-23 16:31:39" runUser="HPELITEONE1000\mrank" xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010">
    '<Times creation = "2020-04-23T16:31:39.2997880+02:00" queuing="2020-04-23T16:31:39.2997880+02:00" start="2020-04-23T16:31:38.7763410+02:00" finish="2020-04-23T16:31:39.4035318+02:00" />
    '<TestSettings name = "default" id="f883f6fb-1112-44bc-bc38-fbd312260b89">
    '<Deployment runDeploymentRoot = "mrank_HPELITEONE1000_2020-04-23_16_31_39" />
    '</TestSettings>
    '<Results>
    '<UnitTestResult executionId = "51ca16c9-40f5-4ff4-898f-e1af4df1c978" testId="4debc440-cfd4-2a84-b7fc-c353aa4dadb9" testName="TST103_ab" computerName="HPELITEONE1000" duration="00:00:00.0012034" startTime="2020-04-23T16:31:39.0773152+02:00" endTime="2020-04-23T16:31:39.0783127+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="51ca16c9-40f5-4ff4-898f-e1af4df1c978" />
    '<UnitTestResult executionId = "1d4b2f54-c30f-4392-b188-ee73045c05b8" testId="3e07dd12-1287-007e-664b-cba4e5156e96" testName="TST105_af" computerName="HPELITEONE1000" duration="00:00:00.0005302" startTime="2020-04-23T16:31:39.2977933+02:00" endTime="2020-04-23T16:31:39.2987902+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="1d4b2f54-c30f-4392-b188-ee73045c05b8" />
    '<UnitTestResult executionId = "b1c675ba-5639-4d2f-96e1-8986438cd45d" testId="1b1c1aad-4781-feb4-9f6c-2a5a7127b7f7" testName="TST105_ag" computerName="HPELITEONE1000" duration="00:00:00.0009128" startTime="2020-04-23T16:31:39.2987902+02:00" endTime="2020-04-23T16:31:39.2997880+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="b1c675ba-5639-4d2f-96e1-8986438cd45d" />
    '<UnitTestResult executionId = "a417e5e7-30e5-470a-ade2-eb10cc2c4e0e" testId="d5bd7eea-afab-06e9-b1f6-555e70e99911" testName="TST105_ak" computerName="HPELITEONE1000" duration="00:00:00.0013706" startTime="2020-04-23T16:31:39.3047461+02:00" endTime="2020-04-23T16:31:39.3057436+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="a417e5e7-30e5-470a-ade2-eb10cc2c4e0e" />
    '<UnitTestResult executionId = "9a064100-210c-4fbd-803b-f0205880ed59" testId="a12d8b5e-1c7c-d9fe-550f-5f898770e8cf" testName="TST105_ad" computerName="HPELITEONE1000" duration="00:00:00.0014067" startTime="2020-04-23T16:31:39.2957984+02:00" endTime="2020-04-23T16:31:39.2967951+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="9a064100-210c-4fbd-803b-f0205880ed59" />
    '<UnitTestResult executionId = "9cee6871-10f5-4f07-baec-3bff24cbbdd6" testId="96fa6d6e-552a-8017-d53a-e614fc52bb74" testName="TST105_ae" computerName="HPELITEONE1000" duration="00:00:00.0006106" startTime="2020-04-23T16:31:39.2967951+02:00" endTime="2020-04-23T16:31:39.2977933+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="9cee6871-10f5-4f07-baec-3bff24cbbdd6" />
    '<UnitTestResult executionId = "301be1c9-dd40-4b0b-b602-f200ee982294" testId="a38c820c-d529-3e73-00d2-87f69294947e" testName="TST105_ab" computerName="HPELITEONE1000" duration="00:00:00.0003978" startTime="2020-04-23T16:31:39.2918093+02:00" endTime="2020-04-23T16:31:39.2918093+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="301be1c9-dd40-4b0b-b602-f200ee982294" />
    '<UnitTestResult executionId = "4d967984-8a09-4957-9cb3-14ba9eed1aad" testId="bdcd91d8-55f7-fb64-2c80-072bffd6a3ce" testName="TST105_aj" computerName="HPELITEONE1000" duration="00:00:00.0009396" startTime="2020-04-23T16:31:39.3037493+02:00" endTime="2020-04-23T16:31:39.3047461+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="4d967984-8a09-4957-9cb3-14ba9eed1aad" />
    '<UnitTestResult executionId = "adc5e5ef-a011-4e36-8c97-7a1547c04693" testId="166ef91f-6c59-284f-bff9-ed1acf43d5d4" testName="TST150_aa" computerName="HPELITEONE1000" duration="00:00:00.1756288" startTime="2020-04-23T16:31:39.1132195+02:00" endTime="2020-04-23T16:31:39.2888230+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="adc5e5ef-a011-4e36-8c97-7a1547c04693" />
    '<UnitTestResult executionId = "f2fa1158-571e-4364-b07c-f86dff194bef" testId="520e82eb-52e6-bf43-8eb2-371731da9c03" testName="TST104_aa" computerName="HPELITEONE1000" duration="00:00:00.0324457" startTime="2020-04-23T16:31:39.0803071+02:00" endTime="2020-04-23T16:31:39.1132195+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="f2fa1158-571e-4364-b07c-f86dff194bef" />
    '<UnitTestResult executionId = "666c664e-055e-409c-8c16-aef7f1d11d89" testId="0b0200be-a85c-9e23-673a-bbf4fef7b57e" testName="TST103_ac" computerName="HPELITEONE1000" duration="00:00:00.0012297" startTime="2020-04-23T16:31:39.0783127+02:00" endTime="2020-04-23T16:31:39.0803071+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="666c664e-055e-409c-8c16-aef7f1d11d89" />
    '<UnitTestResult executionId = "92cf70f9-2167-4ac6-899a-c9bfb0496263" testId="b7a13b45-fb16-5f92-794b-28fdfc353e3e" testName="TST105_aa" computerName="HPELITEONE1000" duration="00:00:00.0022913" startTime="2020-04-23T16:31:39.2888230+02:00" endTime="2020-04-23T16:31:39.2918093+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="92cf70f9-2167-4ac6-899a-c9bfb0496263" />
    '<UnitTestResult executionId = "2fca66b7-da6c-46c3-95de-b88cf6462e88" testId="9a913386-ca20-c65f-0573-36532e5ad4e2" testName="TST105_ah" computerName="HPELITEONE1000" duration="00:00:00.0029401" startTime="2020-04-23T16:31:39.2997880+02:00" endTime="2020-04-23T16:31:39.3027522+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="2fca66b7-da6c-46c3-95de-b88cf6462e88" />
    '<UnitTestResult executionId = "78b1600c-a5bb-4b38-85aa-bba4bbed0afd" testId="c2314cb2-0024-8617-1cb0-743b438b09c6" testName="TST105_ac" computerName="HPELITEONE1000" duration="00:00:00.0007784" startTime="2020-04-23T16:31:39.2918093+02:00" endTime="2020-04-23T16:31:39.2928110+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="78b1600c-a5bb-4b38-85aa-bba4bbed0afd" />
    '<UnitTestResult executionId = "c0b18486-56a2-47df-b510-8d130ed09933" testId="5be39fee-7e6d-99e2-3f48-a011c203f3a4" testName="TST101_aa" computerName="HPELITEONE1000" duration="00:00:00.0161969" startTime="2020-04-23T16:31:39.0055363+02:00" endTime="2020-04-23T16:31:39.0573966+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="c0b18486-56a2-47df-b510-8d130ed09933" />
    '<UnitTestResult executionId = "770275de-53b4-480b-a613-42ba67484027" testId="0058228e-896c-31ca-e450-a76d05794e25" testName="TST105_ai" computerName="HPELITEONE1000" duration="00:00:00.0005129" startTime="2020-04-23T16:31:39.3027522+02:00" endTime="2020-04-23T16:31:39.3037493+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="770275de-53b4-480b-a613-42ba67484027" />
    '<UnitTestResult executionId = "399e01b1-2d26-493f-95bc-a7e3e1c3bdaa" testId="8eb93050-351a-c3a0-034a-7992d018e35a" testName="TST103_aa" computerName="HPELITEONE1000" duration="00:00:00.0131322" startTime="2020-04-23T16:31:39.0633827+02:00" endTime="2020-04-23T16:31:39.0773152+02:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="399e01b1-2d26-493f-95bc-a7e3e1c3bdaa" />
    '</Results>
    '<TestDefinitions>
    '<UnitTest name = "TST105_aj" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="bdcd91d8-55f7-fb64-2c80-072bffd6a3ce">
    '<Execution id = "4d967984-8a09-4957-9cb3-14ba9eed1aad" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_aj"/>
    '</UnitTest>
    '<UnitTest name = "TST103_ab" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="4debc440-cfd4-2a84-b7fc-c353aa4dadb9">
    '<Execution id = "51ca16c9-40f5-4ff4-898f-e1af4df1c978" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST103_converter03" name="TST103_ab"/>
    '</UnitTest>
    '<UnitTest name = "TST105_ae" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="96fa6d6e-552a-8017-d53a-e614fc52bb74">
    '<Execution id = "9cee6871-10f5-4f07-baec-3bff24cbbdd6" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_ae"/>
    '</UnitTest>
    '<UnitTest name = "TST105_ai" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="0058228e-896c-31ca-e450-a76d05794e25">
    '<Execution id = "770275de-53b4-480b-a613-42ba67484027" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_ai"/>
    '</UnitTest>
    '<UnitTest name = "TST105_ah" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="9a913386-ca20-c65f-0573-36532e5ad4e2">
    '<Execution id = "2fca66b7-da6c-46c3-95de-b88cf6462e88" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_ah"/>
    '</UnitTest>
    '<UnitTest name = "TST105_ak" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="d5bd7eea-afab-06e9-b1f6-555e70e99911">
    '<Execution id = "a417e5e7-30e5-470a-ade2-eb10cc2c4e0e" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_ak"/>
    '</UnitTest>
    '<UnitTest name = "TST105_af" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="3e07dd12-1287-007e-664b-cba4e5156e96">
    '<Execution id = "1d4b2f54-c30f-4392-b188-ee73045c05b8" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_af"/>
    '</UnitTest>
    '<UnitTest name = "TST105_ab" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="a38c820c-d529-3e73-00d2-87f69294947e">
    '<Execution id = "301be1c9-dd40-4b0b-b602-f200ee982294" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_ab"/>
    '</UnitTest>
    '<UnitTest name = "TST105_ag" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="1b1c1aad-4781-feb4-9f6c-2a5a7127b7f7">
    '<Execution id = "b1c675ba-5639-4d2f-96e1-8986438cd45d" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_ag"/>
    '</UnitTest>
    '<UnitTest name = "TST105_ad" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="a12d8b5e-1c7c-d9fe-550f-5f898770e8cf">
    '<Execution id = "9a064100-210c-4fbd-803b-f0205880ed59" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_ad"/>
    '</UnitTest>
    '<UnitTest name = "TST105_ac" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="c2314cb2-0024-8617-1cb0-743b438b09c6">
    '<Execution id = "78b1600c-a5bb-4b38-85aa-bba4bbed0afd" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_ac"/>
    '</UnitTest>
    '<UnitTest name = "TST103_ac" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="0b0200be-a85c-9e23-673a-bbf4fef7b57e">
    '<Execution id = "666c664e-055e-409c-8c16-aef7f1d11d89" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST103_converter03" name="TST103_ac"/>
    '</UnitTest>
    '<UnitTest name = "TST150_aa" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="166ef91f-6c59-284f-bff9-ed1acf43d5d4">
    '<Execution id = "adc5e5ef-a011-4e36-8c97-7a1547c04693" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST150_writexml" name="TST150_aa"/>
    '</UnitTest>
    '<UnitTest name = "TST104_aa" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="520e82eb-52e6-bf43-8eb2-371731da9c03">
    '<Execution id = "f2fa1158-571e-4364-b07c-f86dff194bef" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST104_converter04" name="TST104_aa"/>
    '</UnitTest>
    '<UnitTest name = "TST101_aa" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="5be39fee-7e6d-99e2-3f48-a011c203f3a4">
    '<Execution id = "c0b18486-56a2-47df-b510-8d130ed09933" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST101_converter01" name="TST101_aa"/>
    '</UnitTest>
    '<UnitTest name = "TST105_aa" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="b7a13b45-fb16-5f92-794b-28fdfc353e3e">
    '<Execution id = "92cf70f9-2167-4ac6-899a-c9bfb0496263" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST105_converter05" name="TST105_aa"/>
    '</UnitTest>
    '<UnitTest name = "TST103_aa" storage="c:\users\mrank\onedrive - xstrahl ltd\vs-projects\xphys\utpxphys\bin\debug\utpxphys.dll" id="8eb93050-351a-c3a0-034a-7992d018e35a">
    '<Execution id = "399e01b1-2d26-493f-95bc-a7e3e1c3bdaa" />
    '<TestMethod codeBase="C:\Users\mrank\OneDrive - Xstrahl Ltd\VS-Projects\XPHYS\UTPxphys\bin\Debug\UTPxphys.dll" adapterTypeName="executor://mstestadapter/v2" className="UTPxphys.TST103_converter03" name="TST103_aa"/>
    '</UnitTest>
    '</TestDefinitions>
    '<TestEntries>
    '<TestEntry testId = "4debc440-cfd4-2a84-b7fc-c353aa4dadb9" executionId="51ca16c9-40f5-4ff4-898f-e1af4df1c978" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "3e07dd12-1287-007e-664b-cba4e5156e96" executionId="1d4b2f54-c30f-4392-b188-ee73045c05b8" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "1b1c1aad-4781-feb4-9f6c-2a5a7127b7f7" executionId="b1c675ba-5639-4d2f-96e1-8986438cd45d" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "d5bd7eea-afab-06e9-b1f6-555e70e99911" executionId="a417e5e7-30e5-470a-ade2-eb10cc2c4e0e" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "a12d8b5e-1c7c-d9fe-550f-5f898770e8cf" executionId="9a064100-210c-4fbd-803b-f0205880ed59" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "96fa6d6e-552a-8017-d53a-e614fc52bb74" executionId="9cee6871-10f5-4f07-baec-3bff24cbbdd6" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "a38c820c-d529-3e73-00d2-87f69294947e" executionId="301be1c9-dd40-4b0b-b602-f200ee982294" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "bdcd91d8-55f7-fb64-2c80-072bffd6a3ce" executionId="4d967984-8a09-4957-9cb3-14ba9eed1aad" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "166ef91f-6c59-284f-bff9-ed1acf43d5d4" executionId="adc5e5ef-a011-4e36-8c97-7a1547c04693" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "520e82eb-52e6-bf43-8eb2-371731da9c03" executionId="f2fa1158-571e-4364-b07c-f86dff194bef" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "0b0200be-a85c-9e23-673a-bbf4fef7b57e" executionId="666c664e-055e-409c-8c16-aef7f1d11d89" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "b7a13b45-fb16-5f92-794b-28fdfc353e3e" executionId="92cf70f9-2167-4ac6-899a-c9bfb0496263" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "9a913386-ca20-c65f-0573-36532e5ad4e2" executionId="2fca66b7-da6c-46c3-95de-b88cf6462e88" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "c2314cb2-0024-8617-1cb0-743b438b09c6" executionId="78b1600c-a5bb-4b38-85aa-bba4bbed0afd" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "5be39fee-7e6d-99e2-3f48-a011c203f3a4" executionId="c0b18486-56a2-47df-b510-8d130ed09933" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "0058228e-896c-31ca-e450-a76d05794e25" executionId="770275de-53b4-480b-a613-42ba67484027" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestEntry testId = "8eb93050-351a-c3a0-034a-7992d018e35a" executionId="399e01b1-2d26-493f-95bc-a7e3e1c3bdaa" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '</TestEntries>
    '<TestLists>
    '<TestList name = "Results Not in a List" id="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    '<TestList name = "All Loaded Results" id="19431567-8539-422a-85d7-44ee4e166bda" />
    '</TestLists>
    '<ResultSummary outcome = "Completed" >
    '<Counters total="17" executed="17" passed="17" failed="0" error="0" timeout="0" aborted="0" inconclusive="0" passedButRunAborted="0" notRunnable="0" notExecuted="0" disconnected="0" warning="0" completed="0" inProgress="0" pending="0"/>
    '</ResultSummary>
    '</TestRun>

End Class

Public Class TRXtestrun

    Private _id As String
    Private _name As String
    Private _runUser As String

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Property RunUser As String
        Get
            Return _runUser
        End Get
        Set(value As String)
            _runUser = value
        End Set
    End Property
End Class

Public Class TRXtimes

    Private _Creation As DateTime
    Private _queuing As DateTime
    Private _start As DateTime
    Private _finish As DateTime

    Public Property Creation As Date
        Get
            Return _Creation
        End Get
        Set(value As Date)
            _Creation = value
        End Set
    End Property

    Public Property Queuing As Date
        Get
            Return _queuing
        End Get
        Set(value As Date)
            _queuing = value
        End Set
    End Property

    Public Property Start As Date
        Get
            Return _start
        End Get
        Set(value As Date)
            _start = value
        End Set
    End Property

    Public Property Finish As Date
        Get
            Return _finish
        End Get
        Set(value As Date)
            _finish = value
        End Set
    End Property
End Class
Public Class TRXresults

    Private _UnitTestResult As New ObservableCollection(Of TRXUnitTestResult)

    Public Property UnitTestResult As ObservableCollection(Of TRXUnitTestResult)
        Get
            Return _UnitTestResult
        End Get
        Set(value As ObservableCollection(Of TRXUnitTestResult))
            _UnitTestResult = value
        End Set
    End Property
End Class

Public Class TRXUnitTestResult

    Private _executionId As String
    Private _testId As String
    Private _testName As String
    Private _computerName As String
    Private _duration As TimeSpan
    Private _startTime As DateTime
    Private _endTime As DateTime
    Private _testType As String
    Private _outcome As String
    Private _testListID As String
    Private _relativeResultsDirectory As String

    Public Property ExecutionId As String
        Get
            Return _executionId
        End Get
        Set(value As String)
            _executionId = value
        End Set
    End Property

    Public Property TestId As String
        Get
            Return _testId
        End Get
        Set(value As String)
            _testId = value
        End Set
    End Property

    Public Property TestName As String
        Get
            Return _testName
        End Get
        Set(value As String)
            _testName = value
        End Set
    End Property

    Public Property ComputerName As String
        Get
            Return _computerName
        End Get
        Set(value As String)
            _computerName = value
        End Set
    End Property

    Public Property Duration As TimeSpan
        Get
            Return _duration
        End Get
        Set(value As TimeSpan)
            _duration = value
        End Set
    End Property

    Public Property StartTime As Date
        Get
            Return _startTime
        End Get
        Set(value As Date)
            _startTime = value
        End Set
    End Property

    Public Property EndTime As Date
        Get
            Return _endTime
        End Get
        Set(value As Date)
            _endTime = value
        End Set
    End Property

    Public Property TestType As String
        Get
            Return _testType
        End Get
        Set(value As String)
            _testType = value
        End Set
    End Property

    Public Property Outcome As String
        Get
            Return _outcome
        End Get
        Set(value As String)
            _outcome = value
        End Set
    End Property

    Public Property TestListID As String
        Get
            Return _testListID
        End Get
        Set(value As String)
            _testListID = value
        End Set
    End Property

    Public Property RelativeResultsDirectory As String
        Get
            Return _relativeResultsDirectory
        End Get
        Set(value As String)
            _relativeResultsDirectory = value
        End Set
    End Property
End Class