2024-10-28 21:37
Теги: #нейросети

---
# GAN

(Генеративно-состязательная сеть) -  алгоритм машинного обучения, построенный на комбинации из двух нейронных сетей: генеративная модель G, которая строит приближение распределения данных, и дискриминативная модель D, оценивающая вероятность, что образец пришел из тренировочных данных, а не сгенерированных моделью G. Обучение для модели G заключается в максимизации вероятности ошибки дискрминатора D.


![оригинальная архитектура GAN](https://neerc.ifmo.ru/wiki/images/6/6b/%D0%90%D1%80%D1%85_%D0%B3%D0%B0%D0%BD.png)


## О нейросетях

Сначала будет небольшое количество о нейросетях в общем, эту часть можно пропустить

#### Обозначения в формулах
- Ожидаемое значение - **E(θˆm)**
- Истинное базовое значение -  **θ** - которое сгенерированно обучающими данными.
- Точечная оценка - **θˆ**
- Смещение - **bias(θˆm)**
- Дисперсия - **Var(θˆ)**, где случайная величина является обучающим множеством.
- Стандартная ошибка - квадратный корень дисперсии  -  **SE(θˆ)**
- Классификация - **CE**
- Оценка максимального правдоподобия - **MLE**
- Вес нейрона - **w**


#### Полезные термины:

**Оценка** – это статистический термин для нахождения некоторого приближения неизвестного параметра на основе некоторых данных.  

**Точечная оценка** – это попытка найти единственное лучшее приближение некоторого количества интересующих нас параметров. **θˆ**

**Ожидаемая средняя квадратичная ошибка** - это способ измерения того, насколько мы близки к истинному параметру., это квадратичная разница между оценочными и истинными значениями параметров, где математическое ожидание вычисляется над m обучающими выборками из данных, генерирующих распределение. Ни у одной сходящейся функции оценки нет среднеквадратичной ошибки меньше, чем у оценки максимального правдоподобия.

**Многослойный персептрон** — это класс искусственных нейронных сетей прямого распространения, состоящих как минимум из трех слоев: входного, скрытого и выходного. За исключением входных, все нейроны использует нелинейную функцию активации.

![Многослойный персептрон](https://wiki.loginom.ru/images/multilayer-neural-net.svg)

**Математическое ожидание** — понятие в теории вероятностей, означающее среднее значение случайной величины.


разобраться с терминами:
3. задача компрессии данных в низкоразмерное представление
4. вариационных автоэнкодеров
5. генеративная модель
6. сеймпл в нейросети
7. WGAN 
8. UGAN

Схема работы нейросети:
![схема работы нейросети](https://habrastorage.org/r/w1560/files/dad/168/f54/dad168f54a2d4cf0b6508200eda50eef.png)
, где w - разные веса синапсов, то есть насколько надо прислушиваться к получившемуся результату.
Каждый нейрон получает входные данные в границах [0, 1] или [-1, 1] в зависимости от типа нейронной сети. дальше он его обрабатывает и передает значение в тех же пределах дальше.

![пример работы через цвета](https://habrastorage.org/r/w1560/files/6c9/879/ae3/6c9879ae3bf445b3a7e39c0a26598471.png)


#### Функция оценки

Задача, решаемая машинным обучением, заключается в попытке предсказать переменную **y** по заданному входному вектору **x**. Мы предполагаем, что существует функция f(x), которая описывает приблизительную связь между **y** и **x**. Например, можно предположить, что y = f(x) + ε, где ε обозначает часть **y**, которая явно не предсказывается входным вектором **x**. При оценке функций нас интересует приближение f с помощью модели или оценки fˆ. Функция оценки в действительности это тоже самое, что оценка параметра **θ**; функция оценки f это просто точечная оценка в функциональном пространстве.
_Пример: в полиномиальной регрессии мы либо оцениваем параметр **w**, либо оцениваем функцию отображения из **x** в **y**. (то есть или оцениваем вес связи, либо функция оценки, она же точечная оценка)_


#### Смещение и дисперсия

Смещение и дисперсия измеряют два разных источника ошибки функции оценки. Смещение измеряет ожидаемое отклонение от _истинного значения функции_ или _параметра_. Дисперсия, с другой стороны, показывает меру отклонения от _ожидаемого значения оценки_, которую может вызвать любая конкретная выборка данных.

Смещение
Оценщик θˆm называется несмещенным, если bias(θˆm)=0, что подразумевает что E(θˆm) = θ.

**Дисперсия** (стандартная ошибка оценщика)  - показывает меру ожидания того, как оценка, которую мы вычисляем, будет изменяться по мере того, как мы меняем выборки из базового набора данных, генерирующих процесс.


#### Оценка Максимального Правдоподобия (MLE)

Оценка максимального правдоподобия может быть определена как метод оценки параметров (таких как среднее значение или дисперсия) из выборки данных, так что вероятность получения наблюдаемых данных максимальна.

Рассмотрим набор из **m** примеров X={x(1),… , x(m)} взятых независимо из неизвестного набора данных, генерирующих распределение Pdata(x). Пусть Pmodel(x;θ) – параметрическое семейство распределений вероятностей над тем же пространством, индексированное параметром **θ**. Другими словами, Pmodel(x;θ) отображает любую конфигурацию **x** в значение, оценивающее истинную вероятность Pdata(x).
  
Оценка максимального правдоподобия для **θ** определяется как:

![ML](https://id-lab.ru/wp-content/uploads/2019/06/p3.png)


Поскольку мы предположили, что примеры являются  независимыми выборками, приведенное выше уравнение можно записать в виде:

![ML с независимой выборкой](https://id-lab.ru/wp-content/uploads/2019/06/p4.png)

Чтобы получить более удобную, но эквивалентную задачу оптимизации, можно использовать логарифм вероятности, который не меняет его argmax, но удобно превращает произведение в сумму, и поскольку логарифм – строго возрастающая функция (функция натурального логарифма – монотонное преобразование), это не повлияет на итоговое значение **θ**.

![ML через log](https://id-lab.ru/wp-content/uploads/2019/06/p5.png)

**_Именно по причине сходимости и эффективности, оценка максимального правдоподобия часто считается предпочтительным оценщиком для машинного обучения._**


#### Максимальная апостериорная (MAP) оценка

апостериорный - опытный, выведенный из опыта

Оценка MAP выбирает точку максимальной апостериорной вероятности (или максимальной плотности вероятности в более распространенном случае непрерывного θ):

![MAP](https://id-lab.ru/wp-content/uploads/2019/06/p6.png)
, где с правой стороны, log(p(x|θ)) – стандартный член логарифмической вероятности и log(p(θ)) соответствует изначальному распределению.
Эта дополнительная информация помогает уменьшить дисперсию для точечной оценки MAP (по сравнению с оценкой MLE). Однако, это происходит ценой повышенного смещения.


### Функции потерь

В большинстве обучающих сетей ошибка рассчитывается как разница между фактическим выходным значением **y** и прогнозируемым выходным значением **ŷ**. Функция, используемая для вычисления этой ошибки, известна как функция потерь, также часто называемая функцией ошибки или затрат.

**Средняя квадратичная ошибка (MSE):** средняя квадратичная ошибка является наиболее распространенной функцией потерь. Функция потерь MSE широко используется в линейной регрессии в качестве показателя эффективности. Чтобы рассчитать MSE, надо взять разницу между предсказанными значениями и истинными, возвести ее в квадрат и усреднить по всему набору данных.

![MSE](https://id-lab.ru/wp-content/uploads/2019/06/p7.png)
, где y(i) – фактический ожидаемый результат, а ŷ(i) – прогноз модели.

**_Многие функции потерь (затрат), используемые в машинном обучении, включая MSE, могут быть получены из метода максимального правдоподобия._**


**Кросс-энтропия (или логарифмическая функция потерь – log loss):** Кросс-энтропия измеряет расхождение между двумя вероятностными распределениями. Если кросс-энтропия велика, это означает, что разница между двумя распределениями велика, а если кросс-энтропия мала, то распределения похожи друг на друга.

![Кросс-энтропия](https://id-lab.ru/wp-content/uploads/2019/06/p11.png)
, где P – распределение истинных ответов, а Q – распределение вероятностей прогнозов  модели. 


#### Бинарная классификация

M = 2
![Бинарная классификация](https://id-lab.ru/wp-content/uploads/2019/06/p12.png)

При двоичной классификации каждая предсказанная вероятность сравнивается с фактическим значением класса (0 или 1), и вычисляется оценка, которая штрафует вероятность на основе расстояния от ожидаемого значения.


#### Мульти-классовая классификация

M > 2
![Мульти-классовая классификация](https://id-lab.ru/wp-content/uploads/2019/06/p14.png)



## Проблемы обучения GAN

Большинство GAN'ов подвержено следующим проблемам:

- Схлопывание мод распределения (англ. mode collapse): генератор коллапсирует, то есть выдает ограниченное количество разных образцов.
- Проблема стабильности обучения (англ. non-convergence): параметры модели дестабилизируются и не сходятся.
- Исчезающий градиент (англ. diminished gradient): дискриминатор становится слишком "сильным", а градиент генератора исчезает и обучение не происходит.
- Проблема запутывания (англ. disentanglement problem): выявление корреляции в признаках, не связанных (слабо связанных) в реальном мире.
- Высокая чувствительность к гиперпараметрам.

Часть этих проблем будет рассмотрена подробнее ниже, но нужно заметить, что универсального подхода к решению большинства из них нет. Зато существуют практические советы, которые могут помочь при обучении GAN'ов. Основными из них являются:

1. Нормализация данных. Все признаки в диапазоне [−1;1]
2. Замена функции ошибки для G с minlog(1−D) на maxlogD- , потому что исходный вариант имеет маленький градиент на раннем этапе обучения и большой градиент при сходимости, а предложенный наоборот;
3. Сэмплирование из многомерного нормального распределения вместо равномерного;
4.  Использовать нормализационные слои (например, batch normalization или layer normalization) в G и D;
5. Использовать метки для данных, если они имеются, то есть обучать дискриминатор еще и классифицировать образцы.



В процессе обучения генератор может прийти к состоянию, при котором он будет всегда выдавать ограниченный набор выходов. При этом пространство, в котором распределены сгенерированные изображения, окажется существенно меньше, чем пространство исходных изображений. Главная причина этого в том, что генератор обучается обманывать дискриминатор, а не воспроизводить исходное распределение. Если генератор начинает каждый раз выдавать похожий выход, который является максимально правдоподобным для текущего дискриминатора, то зависимость от z падает, а следовательно и градиент G(z) стремиться к 0.

Лучшей стратегией для дискриминатора будет улучшение детектирования этого конкретного изображения. Так на следующих итерациях наиболее вероятно, что генератор придет к другому изображению, хорошо обманывающему текущий дискриминатор, а дискриминатор будет учиться отличать конкретно это новое изображение. Этот процесс не будет сходиться и количество представленных мод не будет расти, поэтому приблизиться к исходному распределению не удастся.

На текущий момент mode collape является одной из главных проблем GAN, эффективное решение которой ещё ищется. Возможные решения проблемы mode collapse:

- WGAN — использование метрики Вассерштейна (англ. Wasserstein Loss) внутри функции ошибки, позволяет дискриминатору быстрее обучаться выявлять повторяющиеся выходы, на которых стабилизируется генератор.
- UGAN (Unrolled GAN) — для генератора используется функция потерь, которая не только от того, как текущий дискриминатор оценивает выходы генератора, но и от выходов будущих версий дискриминатора.


#### Метрики для количественной оценки сходства между двумя распределениями вероятностей

1. **Дивергенция KL (Кульбака-Лейблера)** измеряет, как одно распределение вероятностей **p** отклоняется от второго ожидаемого распределения вероятностей **q**. - Dkl(P || Q). 

		KL(P || Q) = Σ  P(X)log P(X) / Q(X)

Всегда неотрицательное неотрицательное число, достигающим 0, если и только если два рассматриваемых распределения идентичны.
Из формулы видно, что дивергенция KL асимметрична. В случаях, когда p(x) близка к нулю, но q(x) значительно не равен нулю, q эффект игнорируется. Это может привести к ошибочным результатам, когда мы просто хотим измерить сходство между двумя одинаково важными распределениями.
Прочитать больше об этой дивергенцци можно по этой [ссылке](https://en-m-wikipedia-org.translate.goog/wiki/Kullback%E2%80%93Leibler_divergence?_x_tr_sl=en&_x_tr_tl=ru&_x_tr_hl=ru&_x_tr_pto=rq)


2. **Дивергенция Дженсена-Шеннона** — это еще одна мера сходства между двумя распределениями вероятностей, ограниченная. Расхождение JS симметрично и более гладкое.   

		JS(P || Q) = (KL(P || M) + KL(Q || M)) / 2

, где M = (P + Q) / 2 и || представляет оператор конкатенации

Она относительно эффективна в расчетах, что также делает её применимым к большим объемам данных.

С другой стороны, она может быть чувствительна к размеру выборки. Оценки распределений вероятностей могут быть ненадежными, когда размеры выборки малы, и это может повлиять на меру сходства.

Он также может быть менее подходящим, когда распределения сильно различаются. Это связано с тем, что он не улавливает тонкие детали локальных различий.
Больше информации об этой дивергенции можно узнать по этой [ссылке](https://datascientest-com.translate.goog/en/jensen-shannon-divergence-everything-you-need-to-know-about-this-ml-model?_x_tr_sl=en&_x_tr_tl=ru&_x_tr_hl=ru&_x_tr_pto=rq)


## GAN Minimax game

pz - распределение данных по входному шуму z 
pg - распределение генератора по данных x
pr - распределение данных по реальной выборке x

![minimax](https://b2633864.smushcdn.com/2633864/wp-content/uploads/2021/09/5_gans_minimax_game-1024x435.png?lossy=2&strip=1&webp=1)

- Генератор пытается обмануть дискриминатор, заставив его принять поддельные изображения за настоящие
- Дискриминатор пытается правильно классифицировать реальные и поддельные изображения


## Варианты GAN

- DCGAN - первым предложением GAN, использующим сверточную нейронную сеть (CNN) в своей сетевой архитектуре. Большинство современных вариаций GAN в некоторой степени основаны на DCGAN.
- WGAN и WGAN-GP - были созданы для решения задач обучения GAN,  таких как сворачивание режима — когда генератор повторно создает одни и те же изображения или небольшое подмножество обучающих изображений. WGAN-GP улучшает WGAN за счет использования штрафа за градиент вместо ограничения веса для стабильности тренировки.
- ProGAN -  (Прогрессивное наращивание GAN для улучшения качества, стабильности и вариативности) позволяет постепенно расширять сеть.

Самые известные модели GAN направлены на создание изображений по тексту, редактирования видео и фото. Например, вот [ссылка](https://habr.com/ru/articles/726254/) на создание собственного GAN для генерации анимации.


---
### Zero-ссылки
- [[00 Работа]]
---
### Ссылки
- https://education.yandex.ru/handbook/ml/article/generativno-sostyazatelnye-seti-(gan) - главное объяснение GAN
- https://blog.skillfactory.ru/kak-rabotaet-nejronnaya-set-razbiraemsya-s-osnovami/ - менее подробные основы
- https://blog.skillfactory.ru/glossary/deep-learning/ - глубокое изучение, о нескольких слоях нейронов
- https://habr.com/ru/articles/312450/ - основное объяснение
- https://id-lab.ru/posts/developers/funkcii/ - формулы машинного обучения
- https://wiki.loginom.ru/articles/multilayered-perceptron.html - нашла определение 
- многослойного персептрона
- https://neerc.ifmo.ru/wiki/index.php?title=Generative_Adversarial_Nets_(GAN) - информация о GAN
- https://lilianweng.github.io/posts/2017-08-20-gan/ - о GAN и WGAN
- https://yu-xuan.livejournal.com/145896.html - информация о дивергенции Кульбака-Лейблера
- https://datascientest-com.translate.goog/en/jensen-shannon-divergence-everything-you-need-to-know-about-this-ml-model?_x_tr_sl=en&_x_tr_tl=ru&_x_tr_hl=ru&_x_tr_pto=rq - информация о дивергенции Дженсена-Шеннона
- https://pyimagesearch.com/2021/09/13/intro-to-generative-adversarial-networks-gans/ - виды GAN взяты из этой статьи
- https://habr.com/ru/companies/first/articles/753922/ - создание GAN
