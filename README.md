# Inventory

## 유니티 인벤토리 구현 개인 과제

개인 프로젝트로 개발된 **Inventory System**은 Unity 환경에서 플레이어의 캐릭터 정보를 관리하고, 장비 아이템의 획득, 인벤토리 관리, 그리고 장착/해제 기능을 제공합니다.

## 주요 기능 및 특징

### 1. 통합 UI 관리 (UIManager)

* **UI 제어**: `UIManager` 싱글톤을 통해 메인 메뉴, 스테이터스 화면, 인벤토리 화면 등 다양한 UI 패널의 활성화/비활성화 및 전환을 중앙에서 관리합니다.
* **화면 전환**: 버튼 클릭 시 관련 UI 패널이 활성화되고, 다른 패널들은 자동으로 비활성화되어 사용자에게 명확한 UI 흐름을 제공합니다.

### 2. 캐릭터 데이터 관리 (Character)

* **스탯 동적 변경**: 장비 아이템 장착/해제와 같은 특정 액션에 따라 캐릭터의 스탯을 동적으로 추가하거나 감소시키는 메서드를 제공합니다.
* **이벤트 기반 업데이트**: 캐릭터 정보(`OnInfoChanged`) 및 스탯(`OnStatChanged`) 변경 시 이벤트를 발생시켜 UI 등 다른 시스템이 데이터를 반영할 수 있도록 합니다.

### 3. 메인 메뉴 UI (UIMainMenu)

* **플레이어 기본 정보 표시**: 캐릭터의 아이디, 현재 레벨, 경험치, 경험치 바와 같은 핵심 정보를 메인 화면에 시각적으로 표시합니다.
* **핵심 기능 접근**: "Status" 및 "Inventory" 버튼을 통해 플레이어가 상세 스테이터스 화면이나 인벤토리 화면으로 쉽게 전환할 수 있도록 합니다.
* **아이템 추가 기능**: 개발 및 테스트 목적으로 임의의 아이템을 인벤토리에 추가할 수 있는 버튼을 포함합니다. (향후 `InventoryManager`와 연동 예정)

### 4. 상세 스테이터스 UI (UIStatus)

* **실시간 스탯 정보**: 플레이어의 공격력, 방어력, 체력, 치명타율 등 상세 스탯을 텍스트 형태로 표시합니다.
* **자동 업데이트**: `Character` 스크립트의 `OnStatChanged` 이벤트에 구독하여 스탯 변경 시 UI가 즉시 갱신됩니다.
* **뒤로가기**: "뒤로가기" 버튼을 통해 메인 메뉴 화면으로 손쉽게 복귀할 수 있습니다.

### 5. 인벤토리 UI 및 아이템 관리 (UIInventory, UISlot)

* **동적 슬롯 생성**: 초기 설정된 개수 또는 필요에 따라 동적으로 인벤토리 슬롯(`UISlot`)을 생성하고 관리합니다.
* **아이템 표시**: 각 `UISlot`은 할당된 `ItemData`에 따라 아이콘을 표시하고, 장착 여부에 따른 시각적 피드백(아웃라인, 텍스트)을 제공합니다.
* **아이템 장착/해제 시스템**:
    * **슬롯 클릭 상호작용**: `UISlot` 클릭 시 `UIInventory`가 이벤트를 받아 아이템 선택, 장착, 해제 로직을 처리합니다.
    * **동일 타입 장비 관리**: 같은 타입의 장비는 하나만 장착될 수 있도록 기존 장비를 자동으로 해제하고 새 장비를 장착합니다.
    * **스탯 연동**: 장비 장착 시 `Character` 스크립트의 스탯 증가 메서드를 호출하여 캐릭터의 능력치를 즉시 변경하고, 해제 시 원상 복구합니다.
