# 2023 05 24 18:10
아이디어 : cogfit 게임을 차용


주의력, 감각을 측정하는 테스트를 아래의 사이트에서 찾아보시면 테스트가 있습니다. 이 테스트와 건설 현장에서 일어날 만한, 혹은 관련된 컨텐츠를 생각해 오시면 됩니다.
시간이 남으면 나머지 3가지 요소에 대해서도 생각해 봅시다

측정해야할 5가지 요소
* Attention
* Sense
* perception
* Memory
* Decision making

인지 검사 사이트
https://www.cognifit.com/kr/cognitive-assessment/cognitive-test

여기서 만 아래에서 2개인 기억력, 의사결정을 wom-asm 테스트, go/no-go 테스트로 한 것 같음
그 아이디어를 비밀번호 맞추기랑 크레인 움직이기 였던거 같은데, 아직 구현한거 없으니까 더 괜찮은걸로 해도 될듯. 


## 0525

기억력 -> digit span test, 논문 찾음. -> 게임 실행 방식을 논문에서 읽어보고, 방식을 숙지 후 게임을 그렇게 구현하면 됨 

https://www.kci.go.kr/kciportal/ci/sereArticleSearch/ciSereArtiView.kci?sereArticleSearchBean.artiId=ART001191867

의사 결정 -> go/no-go test, 논문과 게임 주제 탐색중.
gpt 대화 결과 참고 필요


## 0616

발표 준비
ppt : hwp 참고해서 작성. PE,BE, 유니티 데모 -> 부분적 통합 데모 시현. 유니티 게임 5개 돌아가는것 보여주기. PE/BE 구축도 따로. 

PPT 구성 : 시스템 설계 부분 -> APP 부분에서 함수 변경. 구현율 색깔 표기 변경. 
지난번에 게임 2개 진행, 이번에 3개 추가로 완성. 

주말까지 유니티 발표자료 완성, 월요일까지 전체 발표자료 완성

유니티는 5개 게임 연결되어 보이도록, 내 게임은 결과 알려줘야 함. -> 어떻게 보여줄지? //합/불합격


### 비밀번호 암기
- 버튼 눌릴 때 숫자 입력한 것 저장
- 숫자 생성
=> 검사 -> 통과시 자릿수 업, 총 2회 기회, 불통과시 게임 종료

- 문제 보여주기 : 3D 오브젝트 활성화/비활성화. 텍스트 한글자씩 보여줄 때 활성화, 끝나면 비활성화 -> 함수처럼 켰다 껐다 하는 스위치 역할?

정방향 입력 : 2-5자리, 역방향 입력 : 2-3자리. 역방향은 문제 보여줄때만 역방향으로 뒤집는 것 필요함. 
합격 조건 : 모든 테스트 통과. 


### Go/No-Go 테스트

0ms
게임 시작 부분, 검은 배경에 중앙에 흰색 십자가
//순서 간 연결 : 시간으로 또는 함수로? 처음 십자가는 시간으로, 그 후는 함수 내부적으로 작동시키면서 도돌이표 돌리기. 

200ms
게임 시작하면 disable 하기

