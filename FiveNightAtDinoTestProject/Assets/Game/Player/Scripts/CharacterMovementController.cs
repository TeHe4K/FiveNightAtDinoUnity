using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f; //Скорость персонажа
    [SerializeField] private float _gravity = -9.8f; //Гравитация персонажа
    [SerializeField] private float _groundDistance = 0.4f; //Дистанция сферы которая определяет когда персонаж на земле
    [SerializeField] private float _jumpHeight = 3f; //Высота прыжка

    private bool _isGrounded; //Находится ли персонаж на земле
    private CharacterController _characterController;//Переменная где будет хранится CharacterController
    private Animator _animator; // компонент для управления анимациями
    private Animator _camera;

    public Transform _groundChecker; //Обьект для понимания находится ли персонаж на земле
    public LayerMask _groundMask; //Слой который считается за землю

    Vector3 _velocity; //Ускорение

    private void Start()  
    {
    _camera = GameObject.Find("PlayerCamera").GetComponent<Animator>();
    _characterController = GameObject.FindWithTag("Player").GetComponent<CharacterController>(); //ищем компонент CharacterController
    _animator = GameObject.FindWithTag("Player").GetComponent<Animator>(); //ищем компонент animator
    }

    private void Update()  
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position , _groundDistance , _groundMask); //Создается маленькая сфера которая определяет находится ли персонаж на земле
        if(_isGrounded && _velocity.y < 0) //Если персонаж на земле и ускорение больше 0 то ускорение ставим на -2f что бы при прыжке он не падал со скоростью света
        {
            _camera.SetBool("IsGroundedCamera",true);
            _animator.SetBool("InGrounded",true);
            _velocity.y = -2f;
        }

        float _x = Input.GetAxis("Horizontal"); // Создаем новую переменную _x и записывает туда управление по горизонтали
        float _z = Input.GetAxis("Vertical"); // Создаем новую переменную _y и записывает туда управление по вертикали

        Vector3 _playerMovement = transform.right *_x + transform.forward * _z; //Формула ходьбы
        _characterController.Move(_playerMovement * _speed * Time.deltaTime); //Запихиваем передвежение персонажа в _characterController
        _animator.SetFloat("Z",_z);
        _animator.SetFloat("X",_x);
        _velocity.y += _gravity * Time.deltaTime; //формула падения
        _characterController.Move(_velocity * Time.deltaTime); //Запихиваем формулу падения в _characterController.Так же по этой формуле ускорение умножено дважды на время из за этого мы умножаем _velocity еще раз на Time.deltaTime

        if(Input.GetButtonDown("Jump") && _isGrounded) //Прыжок
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity); //По формуле прыжок это сила прыжка умноженая на -2 умноженая на гравитацию в корне
            _animator.SetBool("InGrounded",false);
            _camera.SetBool("IsGroundedCamera",false);

        }
    }
}
