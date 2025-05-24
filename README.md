🏭 SmartFactory - MES 관리 시스템 (C# WinForms 기반)

✨ C# WinForms 기반으로 제작된 MES 시스템은 관리자 및 일반 사용자가 로그인하여 공장 운영을 관리할 수 있는 스마트 팩토리 솔루션입니다.



🚀 핵심 기능 요약

👤 1. 사용자 인증

![image](https://github.com/user-attachments/assets/191dd773-904f-43f1-8a2a-f48c8e302d2e)



🔐 사용자는 회원가입 또는 로그인을 통해 시스템에 진입할 수 있습니다.

로그인 화면에서는 아이디 / 비밀번호 / 직원코드를 입력받습니다.

IsActive = true인 사용자만 로그인 가능하며, 비활성화 시 접속 제한

로그인 시 자동으로 수행되는 동작:

Users 테이블의 LastLogin 갱신

AttendanceStatus: 퇴근 → 출근으로 변경

AttendanceStatus 테이블에 출근 기록 추가

오전 9시 이후 출근 시 → 지각 기록 (하루 1회만 기록)

📝 2. 회원가입

📄 회원가입 화면에서는 다음 정보를 입력:

👤 아이디

🙋 성명 (FullName)

🏢 부서명 (Department)

🆔 직원코드 (EmployeeCode)

🔒 비밀번호 (Password)

정상 입력 시 Users 테이블에 계정 등록

🖥️ 3. 메인 대시보드 (로그인 성공 후)

로그인 후 메인보드(📷 5번 이미지)로 이동하며 다음 메뉴 제공:

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

📈 1. 개요 (Dashboard Overview)

부서별 생산량 그래프 📊

불량률 추이 선그래프 📉

자재 구성 파이차트 🥧

오늘 지시 수 / 평균 불량률 / 완료 건 등 요약 정보 제공

🏭 2. 생산관리

생산 지시 및 실적 정보를 관리:

📦 제품코드

🔢 지시수량 / 생산수량 / 불량수량

🗓️ 납기일 / 실적일

📋 지시상태 / 달성률

📦 3. 자재관리

자재 정보 통합 관리:

📊 자재별 재고 수량 (MaterialInventory)

🔄 자재별 입출 이력 (MaterialTransactions)

📉 재고 부족 / 입출 건수 / 총 자재 수 요약

📅 4. 일정 / 스케쥴 관리

생산/자재 관련 전반적인 일정 스케쥴 등록 및 조회 제공

🛠️ 관리자 메뉴 (Admin Only)

Role = Admin만 접근 가능 (권한 체크)

기능:

🧾 사용자 목록 확인

🚫 사용자 계정 비활성화 (IsActive = false)

권한 없을 시 ❌ 메시지박스 후 진입 차단

🧾 5.1 자재 등록 관리

자재 등록 시:

MaterialInventory에 자재코드, 재고수량, 최근 업데이트 자동 입력

📋 5.2 인벤토리 입출 이력 관리

MaterialTransactions에 입출 이력 저장:

항목: 자재번호, 입출 유형(IN/OUT), 수량, 날짜, 비고

🔚 로그아웃 기능

로그아웃 시:

Users.AttendanceStatus → 출근 → 퇴근으로 갱신

AttendanceStatus.LogoutTime → 현재 시점으로 업데이트

🛠️ 기술 및 구조 요약

🖥️ C# WinForms

🗃️ MS SQL Server (MYDB2)

주요 테이블:

Users: 사용자 정보 / 인증

AttendanceStatus: 출퇴근 / 지각 기록

MaterialInventory: 자재 재고 현황

MaterialTransactions: 입출고 이력

🧠 핵심 로직 요약

⏰ 지각 판정 기준: 오전 9시 이후 출근

🗓️ 지각 기록은 하루 1회만 허용

🔒 관리자 메뉴: Role != Admin 접근 차단

❌ 비활성 계정: 로그인 불가 (IsActive = false)

▶️ 실행 방법

MSSQL에 MYDB2 데이터베이스 생성

Visual Studio에서 WinForms 프로젝트 실행

기본 로그인 정보 예시:
