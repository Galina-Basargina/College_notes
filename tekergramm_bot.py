# Information: https://clover.coex.tech/en/snippets.html#navigate_wait
import math
import rospy
from clover import srv
from std_srvs.srv import Trigger
import telebot
from time import sleep




token = "???"
bot = telebot.TeleBot(token, parse_mode=None)
weight_price = None
delivery_price = None
street_marker = None
flag = False

@bot.message_handler(commands=['start', 'help'])
def send_welcome(message):
        bot.reply_to(message,
"""Здравствуй!
Сколько будет весить твой груз? 1, 2 или 3 кг?
Ты можешь заказать доставку на ул. Ватутина, ул. Садовая, Витебский пр. и Невский пр.
Будь внимателен - напиши в точности так же как написано в списке!
Ты можешь отменить заказ написав 'Отмена заказа'"""
   )

@bot.message_handler(func=lambda m: True)
def echo_all(message):
    rospy.init_node('flight')

    get_telemetry = rospy.ServiceProxy('get_telemetry', srv.GetTelemetry)
    navigate = rospy.ServiceProxy('navigate', srv.Navigate)
    navigate_global = rospy.ServiceProxy('navigate_global', srv.NavigateGlobal)
    set_position = rospy.ServiceProxy('set_position', srv.SetPosition)
    set_velocity = rospy.ServiceProxy('set_velocity', srv.SetVelocity)
    set_attitude = rospy.ServiceProxy('set_attitude', srv.SetAttitude)
    set_rates = rospy.ServiceProxy('set_rates', srv.SetRates)
    land = rospy.ServiceProxy('land', Trigger)
    
    global delivery_price
    global weight_price
    global street_marker
    global flag

    text = ''

    if message.text == "1":
        text = "Ты заказал доставку на 1 кг\n"
        weight_price = 300
        flag = True
    elif message.text == "2":
        text = "Ты заказал доставку на 2 кг\n"
        weight_price = 400
        flag = True
    elif message.text == "3":
        text = "Ты заказал доставку на 3 кг\n"
        weight_price = 500
        flag = True
    
    elif message.text == "ул. Ватутина":
        text = "Ты выбрал адрес ул. Ватутина\n"
        street_marker = 'aruco_12'
        delivery_price = 600
    elif message.text == "Витебский пр.":
        text = "Ты выбрал адрес Витебский пр.\n"
        street_marker = 'aruco_2'
        delivery_price = 800
    elif message.text == "ул. Садовая":
        text = "Ты выбрал адрес ул. Садовая\n"
        street_marker = 'aruco_7'
        delivery_price = 600
    elif message.text == "Невский пр.":
        text = "Ты выбрал адрес Невский пр.\n"
        street_marker = 'aruco_17'
        delivery_price = 800

    elif message.text == "Отмена заказа":
            weight_price = None
            delivery_price = None
            street_marker = None
            bot.reply_to(message, "Вы отменили свой заказ")
            return

    else:
        bot.reply_to(message, "Прости, но я не понял что ты написал. Проверь свое сообщение")
        return


    if weight_price is None:
        bot.reply_to(message, text+"Ты не выбрал вес груза, для продолжения требуется его указать")
        return
    if delivery_price is None:
        bot.reply_to(message, text+"Ты не выбрал адрес доставки, для продолжения требуется его указать")
        return
    bot.reply_to(message, text+"Стоимость доставки: "+str(delivery_price+weight_price))

    text = ''


    def flight (marker):
        navigate(x=0, y=0, z=0.5, frame_id='body', auto_arm=True)
        print("1/6")
        rospy.sleep(6)
        navigate(x=0, y=0, z=0.5, frame_id=marker)
        print("2/6")
        rospy.sleep(6)
        land()
        print("3/6")
        rospy.sleep(10)
        navigate(x=0, y=0, z=0.5, frame_id='body', auto_arm=True)
        print("4/6")
        rospy.sleep(6)
        navigate(x=0, y=0, z=0.5, frame_id='aruco_0')
        print("5/6")
        rospy.sleep(6)
        land()
        print("6/6")
        rospy.sleep(10)

    if (not street_marker is None) and flag:
        # flight(street_marker)
        print(street_marker)
        print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
        weight_price = None
        delivery_price = None
        street_marker = None
        flag = False

        
    
bot.infinity_polling()
