#  .NET/C# 開發實戰 C# 非同步練習範例

|專案名稱|專案說明|備註|
|-|-|-|
|C001.封鎖應用程式執行AsyncWaitHandle|使用 APM 非同步方法，但為同步設計 1|使用同步設計方式，請求非同步呼叫存取網路服務，進行計算兩個整數值相加結果為何，但使用 AsyncWaitHandle.WaitOne() 來等候執行結果|
|C002.封鎖應用程式執行結束非同步作業方式|使用 APM 非同步方法，但為同步設計 2|以結束非同步作業的方式封鎖應用程式執行，無法在等候非同步作業的結果時繼續執行其他工作的應用程式必須封鎖，直到作業完成為止|
|C003.輪詢非同步作業的狀態|使用 APM 非同步方法，但為同步設計 3|輪詢非同步作業的狀態，以便得知該非同步作業是否已經完成了|
|C004.AsyncCallback委派結束非同步作業|使用 APM 非同步方法，但為同步設計 4|使用 AsyncCallback 委派結束非同步作業|
|C005.APM非同步程式設計模型|了解APM非同步程式設計模型的使用方式|使用了 HttpRequest 來進行讀取遠端網頁內容，透過 HttpWebRequest 非同步 APM 方法 BeginGetResponse 方法啟動非同步的呼叫，完成後，使用 EndGetResponse 結束非同步呼叫並且取得結果內容|
|C006.EAP事件架構非同步模式|了解EAP非同步程式設計模型的使用方式|使用 WebClient 類別來做為 EAP 設計方法的練習說明|
|C007.TAP以工作為基礎的非同步模式|了解TAP非同步程式設計模型的使用方式|使用 TAP Task-based Asynchronous Pattern 來呼叫非同步的工作|
||||
||||
||||
||||
||||
||||
||||
||||
|-|-|-|
||||


