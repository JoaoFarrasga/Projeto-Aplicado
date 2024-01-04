# Projeto-Aplicado

## Técnicas de Inteligência Artificial Utilizadas

### Enemy State Machine


No nosso jogo, utilizamos uma Enemy State Machine para ordenar os estados que cada inimigo possui.


![1704404889403](image/README/1704404889403.png)


O Enemy Controller é usado para armazenar os dados dos inimigos, como a sua velocidade e dano por exemplo.


![1704404917871](image/README/1704404917871.png)


Temos também a opção de guardar se o inimigo tem patrol ou chase state, pois pode ser que não tenha.

No nosso jogo a vida dos inimigos e do jogador é tempo, e está constantemente a diminuir, por isso caso o inimigo chegue a 0 de vida, o state dele passa a ser o deathState.


![1704404939237](image/README/1704404939237.png)


A função que troca cada estado, quando não houver nenhum estado ativo a state machine mete o idleState por norma.

Os inimigos que têm patrol state estão com esse state ativo por norma, caso o inimigo possua chase state e o player entre no seu campo de visão, o state passa a ser o chase state

Caso o inimigo esteja em idle state este troca para chase state da mesma forma, e caso o inimigo tenha patrol state, este automaticamente passa de idle para patrol state.

E durante o chase state, caso o inimigo perca o jogador do seu campo de visão, ele volta para o idle state.


### Dugeon Generator


### Pathfinding A*
