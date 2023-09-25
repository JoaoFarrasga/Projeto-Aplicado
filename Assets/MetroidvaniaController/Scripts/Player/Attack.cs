using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float dmgValue = 4;
	public GameObject throwableObject;
	public Transform attackCheck;
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
	public bool isTimeToCheck = false;
	public bool isAttacking = false;
	public Weapon pointer;

	public bool isAttackingLight = false;
	public bool isAttackingHeavy = false;

	public GameObject cam;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack && !isAttacking)
		{
			isAttacking = true;
			canAttack = false;
			animator.SetFloat("AttackSpeed", pointer.lightAttackSpeed);
			animator.SetBool("IsAttacking", true);
			StartCoroutine(LightAttackCooldown());
		}

		if (Input.GetKeyDown(KeyCode.Mouse1) && canAttack && !isAttacking)
		{
			/*
			GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f,-0.2f), Quaternion.identity) as GameObject; 
			Vector2 direction = new Vector2(transform.localScale.x, 0);
			throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction; 
			throwableWeapon.name = "ThrowableWeapon";
			*/
			isAttacking = true;
			canAttack = false;
			animator.SetFloat("AttackSpeed", pointer.heavyAttackSpeed);
			animator.SetBool("IsAttacking", true);
			StartCoroutine(HeavyAttackCooldown());

		}
	}

	IEnumerator LightAttackCooldown()
	{
		yield return new WaitForSeconds(pointer.lightCooldown);
		isAttacking = false;
		canAttack = true;
	}
	IEnumerator HeavyAttackCooldown()
	{
		yield return new WaitForSeconds(pointer.heavyCooldown);
		isAttacking = false;
		canAttack = true;
	}

	public void DoDashDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
				{
					dmgValue = -dmgValue;
				}
				collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
				cam.GetComponent<CameraFollow>().ShakeCamera();
			}
		}
	}
}
