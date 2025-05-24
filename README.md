# 🏭 SmartFactory - MES 관리 시스템 (C# WinForms 기반)

## ✨ 프로젝트 소개
**C# WinForms 기반 MES 시스템**으로, 관리자와 사용자가 로그인하여 공장 운영을 효율적으로 관리할 수 있는 **스마트 팩토리 솔루션**입니다.

---

## 📸 시스템 이미지 예시

<br>

![로그인 화면](https://github.com/user-attachments/assets/74b3e178-0fd9-4bac-8f0f-818867fad817)

<br>

---

## 🚀 핵심 기능 요약

### 👤 사용자 인증

<br>

![사용자 인증](https://github.com/user-attachments/assets/191dd773-904f-43f1-8a2a-f48c8e302d2e)

<br>

**기능 설명:**
- 로그인 시 `아이디 / 비밀번호 / 직원코드` 입력
- `IsActive = true` 인 사용자만 로그인 가능
- 로그인 시 아래 동작 자동 수행:
  - `Users.LastLogin` 갱신
  - `AttendanceStatus`: 퇴근 → 출근
  - 오전 9시 이후 출근 시 `지각` 기록 (1일 1회)

---

### 📝 회원가입

<br>

![회원가입 화면](https://github.com/user-attachments/assets/9237dbe6-aa40-438d-b065-1442803984f6)

<br>

**입력 항목:**
- 👤 아이디
- 🙋 성명
- 🏢 부서명
- 🆔 직원코드
- 🔒 비밀번호

정상 입력 시 `Users` 테이블에 계정 등록됨

---

### 🖥️ 메인 대시보드 (로그인 성공 후)

<br>

![메인 보드](https://github.com/user-attachments/assets/45b44085-31c9-44b6-8967-64bcba338e0d)

<br>

**접근 가능한 메뉴:**
- 📌 개요
- 🏭 생산관리
- 📦 자재관리
- 🛠️ 설비관리
- 🧑‍💼 인력관리
- 📅 일정 / 스케쥴 관리
- 📊 리포트 / 통계
- ⚙️ 시스템 설정
- 🛡️ 관리자 메뉴
- 🔓 로그아웃

---

## 📊 주요 메뉴 기능

### 📈 개요 (Dashboard)
- 부서별 생산량 그래프 📊  
- 불량률 추이 선그래프 📉  
- 자재 구성 비율 파이차트 🥧  
- 오늘 지시 수 / 평균 불량률 / 완료 건 요약

---

### 🏭 생산관리

<br>

![생산 관리](https://github.com/user-attachments/assets/53a338ac-cf69-4d9e-8c40-75b6bad5b1ee)

<br>

- 📦 제품코드
- 🔢 지시수량 / 생산수량 / 불량수량
- 🗓️ 납기일 / 실적일
- 📋 지시상태 / 달성률

---

### 📦 자재관리

<br>

![자재 관리](https://github.com/user-attachments/assets/437fd60f-768f-422c-bfd5-59cd54d80fe4)

<br>

- 📊 자재별 재고 수량 (MaterialInventory)
- 🔁 입출 이력 (MaterialTransactions)
- 📉 재고 부족 / 입출 건수 / 총 자재 수 요약

---

### 🧑‍💼 인력관리

<br>

![인력 관리](https://github.com/user-attachments/assets/6befdf77-b705-416e-b958-36b6bfa3b525)

<br>

- 사용자의 정보 및 상태 확인 가능

---

### 🛡️ 관리자 메뉴 (Admin 전용)

<br>

![관리자 메뉴](https://github.com/user-attachments/assets/c7a53868-b3d8-4e0e-9fbd-d08e8742480b)

<br>

**관리자 전용 기능:**
- 🧾 사용자 목록 조회
- 🚫 계정 비활성화 (IsActive = false)
- ❌ 권한 없는 경우 메시지박스 출력 및 접근 제한

---

#### 🧾 자재 등록 관리
- 등록 시 `MaterialInventory`에 자재코드, 수량, 업데이트일 자동 입력

---

#### 📋 인벤토리 입출 이력

<br>

![인벤토리 이력](https://github.com/user-attachments/assets/ce293b0f-4a5c-4168-87d3-677f90491758)

<br>

- 자재번호, 입출 유형(IN/OUT), 수량, 일자, 비고 저장

---

### 🔚 로그아웃 기능
- 로그아웃 시 `AttendanceStatus` → 퇴근 처리
- `LogoutTime`이 현재 시점으로 기록됨

---

## 🛠️ 기술 및 구조 요약

### 🖥️ 기술 스택
- **C# WinForms**
- **MS SQL Server (MYDB2)**

### 🗃️ 테이블 요약

| 테이블 | 설명 |
|--------|------|
| 🧑‍💼 `Users` | 사용자 로그인 및 인증 정보 |
| ⏰ `AttendanceStatus` | 출퇴근 및 지각 여부 기록 |
| 🧾 `Workers` | 직원 기본 정보 |
| 📦 `Materials` | 자재명, 단위, 분류, 안전재고 |
| 📊 `MaterialInventory` | 현재 재고 수량, 업데이트 일시 |
| 🔁 `MaterialTransactions` | 자재 입출고 이력 |
| 📋 `ProductionOrders` | 생산 지시 내용 |
| 🏭 `ProductionResults` | 생산 실적 내용 |

---

## 🧠 핵심 로직 요약

- ⏰ **지각 기준**: 오전 9시 이후 출근 시
- 📅 **지각 기록은 하루 1회만 허용**
- 🔒 관리자 메뉴는 `Role != Admin`일 경우 접근 차단
- ❌ `IsActive = false`인 계정은 로그인 제한

---

## ▶️ 실행 방법

```bash
1. MSSQL에서 MYDB2 데이터베이스 생성
2. Visual Studio에서 WinForms 프로젝트 실행
3. 기본 로그인 정보:

ID: thaud153
PW: dkdlzks!153
직원코드: 1114
