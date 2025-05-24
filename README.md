# 🏭 SmartFactory - MES 관리 시스템 (C# WinForms 기반)

## ✨ 프로젝트 소개
**C# WinForms 기반 MES 시스템**으로, 관리자와 사용자가 로그인하여 공장 운영을 관리할 수 있는 스마트 팩토리 솔루션입니다.

---

## 📸 시스템 이미지


![image](https://github.com/user-attachments/assets/74b3e178-0fd9-4bac-8f0f-818867fad817)




## 🚀 핵심 기능 요약

### 👤 1. 사용자 인증



![image](https://github.com/user-attachments/assets/191dd773-904f-43f1-8a2a-f48c8e302d2e)



###🔐 사용자는 회원가입 또는 로그인을 통해 시스템에 진입할 수 있습니다.



로그인 화면에서는 아이디 / 비밀번호 / 직원코드를 입력받습니다.

IsActive = true인 사용자만 로그인 가능하며, 비활성화 시 접속 제한

로그인 시 자동으로 수행되는 동작:

Users 테이블의 LastLogin 갱신

AttendanceStatus: 퇴근 → 출근으로 변경

AttendanceStatus 테이블에 출근 기록 추가

오전 9시 이후 출근 시 → 지각 기록 (하루 1회만 기록)

### 📝 2. 회원가입



![image](https://github.com/user-attachments/assets/9237dbe6-aa40-438d-b065-1442803984f6)


📄 회원가입 화면에서는 다음 정보를 입력:

👤 아이디

🙋 성명 (FullName)

🏢 부서명 (Department)

🆔 직원코드 (EmployeeCode)

🔒 비밀번호 (Password)

정상 입력 시 Users 테이블에 계정 등록

### 🖥️ 3. 메인 대시보드 (로그인 후)







![image](https://github.com/user-attachments/assets/45b44085-31c9-44b6-8967-64bcba338e0d)






로그인 후 메인보드로로 이동하며 다음 메뉴 제공:

📌 개요

🏭 생산관리

📦 자재관리

🛠️ 설비관리

🧑‍💼 인력관리

📅 일정/스케쥴 관리

📊 리포트/통계

⚙️ 시스템 설정

🛡️ 관리자 메뉴

🔓 로그아웃

📊 세부 메뉴 설명





## 📊 주요 메뉴 기능

### 📈 개요 (Dashboard)

부서별 생산량 그래프 📊

불량률 추이 선그래프 📉

자재 구성 파이차트 🥧

오늘 지시 수 / 평균 불량률 / 완료 건 등 요약 정보 제공

### 🏭 생산관리




![image](https://github.com/user-attachments/assets/53a338ac-cf69-4d9e-8c40-75b6bad5b1ee)


생산 지시 및 실적 정보를 관리:

### 📦 제품코드




🔢 지시수량 / 생산수량 / 불량수량

🗓️ 납기일 / 실적일

📋 지시상태 / 달성률

### 📦 자재관리

![image](https://github.com/user-attachments/assets/437fd60f-768f-422c-bfd5-59cd54d80fe4)


자재 정보 통합 관리:

📊 자재별 재고 수량 (MaterialInventory)

🔄 자재별 입출 이력 (MaterialTransactions)

📉 재고 부족 / 입출 건수 / 총 자재 수 요약

### 🧑‍💼 인력관리




![image](https://github.com/user-attachments/assets/6befdf77-b705-416e-b958-36b6bfa3b525)


사용자의 정보 및 상태등을 볼 수 있음

### 🛡️ 관리자 메뉴 (Admin 전용)

![image](https://github.com/user-attachments/assets/c7a53868-b3d8-4e0e-9fbd-d08e8742480b)


Role = Admin만 접근 가능 (권한 체크)

기능:

🧾 사용자 목록 확인

🚫 사용자 계정 비활성화 (IsActive = false)

권한 없을 시 ❌ 메시지박스 후 진입 차단

###🧾 5.1 자재 등록 관리



#### 🧾 자재 등록 관리




MaterialInventory에 자재코드, 재고수량, 최근 업데이트 자동 입력

###📋 5.2 인벤토리 입출 이력 관리

![image](https://github.com/user-attachments/assets/ce293b0f-4a5c-4168-87d3-677f90491758)


MaterialTransactions에 입출 이력 저장:

항목: 자재번호, 입출 유형(IN/OUT), 수량, 날짜, 비고

#### 🔚 로그아웃 기능

로그아웃 시:

Users.AttendanceStatus → 출근 → 퇴근으로 갱신

AttendanceStatus.LogoutTime → 현재 시점으로 업데이트

🛠️ 기술 및 구조 요약

#### 🖥️ C# WinForms

주요 테이블 구조

#### 🧑‍💼 Users: 사용자 인증 및 로그인 정보

Username, Password, Role, Department, IsActive, LastLogin 등 포함

#### ⏰ AttendanceStatus: 출퇴근 기록 및 지각 여부

LoginTime, LogoutTime, PunctualityStatus 등

#### 🧾 Workers: 직원 기본 정보 테이블

UserID, WorkerCode, Department, Shift, HireDate, SkillLevel

#### 📦 Materials: 자재 기본 정보

MaterialName, Unit, Category, SafetyStock (안전재고)

#### 📊 MaterialInventory: 자재 재고 현황

StockQty, LastUpdate 등 포함

#### 🔁 MaterialTransactions: 자재 입출고 이력

TransactionType(IN/OUT), Quantity, TransactionDate, Note

#### 📋 ProductionOrders: 생산 지시 정보

ProductCode, Quantity, Status, OrderDate, DueDate, Department 등

#### 🏭 ProductionResults: 생산 실적 기록

ProducedQty, DefectQty, ResultDate 등

🧠 핵심 로직 요약

⏰ 지각 판정 기준: 오전 9시 이후 출근

🗓️ 지각 기록은 하루 1회만 허용

🔒 관리자 메뉴: Role != Admin 접근 차단

❌ 비활성 계정: 로그인 불가 (IsActive = false)

▶️ 실행 방법

MSSQL에 MYDB2 데이터베이스 생성

Visual Studio에서 WinForms 프로젝트 실행

기본 로그인 정보 예시:
